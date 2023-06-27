
clear

$sql = "
DELETE FROM Milestones
DELETE FROM Organizers
DELETE FROM Precons
DELETE FROM Rooms
DELETE FROM Sponsors
DELETE FROM Events
"

Invoke-DbaQuery -SqlInstance "localhost" -Database "datasaturdays" -Query $sql

$colMap = @{
    id = "event_id"; 
    name = "name";
    date = "event_date"; 
    virtual = "virtual"; 
    description = "description"; 
    registrationurl = "registration_url"; 
    callforspeakers = "callforspeakers_url"; 
    scheduleurl = "schedule_url"; 
    speakerlisturl = "speaker_list_url"; 
    volunteerrequesturl = "volunteer_url"; 
    'hide-toplogo' = "hide_top_logo"; 
    'hide-join-room-section' = "hide_join_room"; 
    'open-registration-newtab'= "open_registration_new_tab"; 
    scheduleapp = "schedule_app"; 
    venuemap = "venue_map"; 
    codeofconduct = "code_of_conduct"; 
    sponsorbenefits = "sponsor_benefits"; 
    'sponsor-menuitem' = "sponsor_menuitem"; 
    'sponsorpack-link' = "sponsorpack_link";
    precondescription = "precon_description";
}

Get-ChildItem "C:\GitHub\DataSaturdays\_data\events" | % {
    $currentFile = $_
    $theGuid = New-Guid
    $yaml = Get-Content $_.FullName -Raw -Encoding UTF8

    $hash = ConvertFrom-Yaml $yaml
    if($hash){

        #remove attributes with _No response_
        $toRemove = @()
        $hash.Keys | % {
            if($hash[$_]) {
                if($hash[$_].ToString().Equals("_No response_")) {
                    $toRemove += $_
                }
            }
        }
        if($toRemove) {
            $toRemove | % {
                $hash.Remove($_)
            }
        }

        $hash.add("id",$theGuid)
        if(-not $hash.ContainsKey("virtual")){
            $hash.add("virtual",$false)
        }
        if($hash.ContainsKey("callforspeakers")){
            $callForSpeakers = $hash["callforspeakers"]
            if($callForSpeakers -eq "true"){
                $hash["callforspeakers"] = $hash["speakerlisturl"]
            }
            else {
                $hash.Remove("callforspeakers")
            }
        }
        $custObj = [pscustomobject] $hash
        $dt = $custObj | ConvertTo-DbaDataTable
    }
    
    
    $filteredColMap = @{}
    if($hash){
        $theKeys = $colMap.Keys | Where-Object {$hash.ContainsKey($_)}
        $theKeys | % {$filteredColMap.Add($_,$colMap[$_])}
        #$filteredColMap
        $dt | Write-DbaDataTable -SqlInstance "localhost" -Database "datasaturdays" -Table "Events" -ColumnMap $filteredcolMap
    }

    #----------------------- Organizers
    $orgColMap = @{
        organizer_id = "organizer_id"; 
        event_id = "event_id";
        name = "name"; 
        email = "email"; 
        twitter = "twitter"; 
    }
    if($hash.ContainsKey("Organizers")){
        $hash.Organizers | % {
            $org = $_

            if($org.email){ #exclude organizers with no email

                $org.email = $org.email.Replace("mailto:","")

                if(-not $org.ContainsKey("event_id")) {
                    $org.Add("event_id",$theGuid)
                }
                if(-not $org.ContainsKey("organizer_id")) {
                    $org.Add("organizer_id",(new-guid))
                }
                $dt = ([pscustomobject]$org) | ConvertTo-DbaDataTable

                $filteredColMap = $orgColMap
                if(-not $org.ContainsKey("twitter")) {
                    $filteredColMap.Remove("twitter")
                }
                #$dt
                $dt | Write-DbaDataTable -SqlInstance "localhost" -Database "datasaturdays" -Table "Organizers" -ColumnMap $filteredColMap
            }
        }
    }


    #----------------------- Rooms
    $roomColMap = @{
        room_id = "room_id"; 
        event_id = "event_id";
        name = "name"; 
        url = "URL"; 
    }
    if($hash.ContainsKey("join")){
        $hash.Join.Rooms | % {
            $room = $_
            if($room){
                if(-not $room.ContainsKey("event_id")) {
                    $room.Add("event_id",$theGuid)
                }
                if(-not $room.ContainsKey("room_id")) {
                    $room.Add("room_id",(new-guid))
                }
                $dt = ([pscustomobject]$room) | ConvertTo-DbaDataTable
                
                $filteredColMap = $roomColMap
                if(-not $room.ContainsKey("url")) {
                    $filteredColMap.Remove("url")
                }
                #$dt
                $dt | Write-DbaDataTable -SqlInstance "localhost" -Database "datasaturdays" -Table "Rooms" -ColumnMap $filteredColMap
            }
        }
    }



    #----------------------- Sponsors
    $sponsorColMap = @{
        sponsor_id = "sponsor_id"; 
        event_id = "event_id";
        name = "name"; 
        sponsorlevel = "sponsor_level"; 
        image = "image_url";
        link = "link_url";
        width = "width_px";
        height = "height_px";
        'margin-top' = "margin_top_px";
        'margin-bottom' = "margin_bottom_px";
    }
    if($hash.ContainsKey("sponsors")){
        $hash.sponsors | % {
            $sponsor = $_
            if($sponsor){
                if(-not $sponsor.ContainsKey("event_id")) {
                    $sponsor.Add("event_id",$theGuid)
                }
                if(-not $sponsor.ContainsKey("sponsor_id")) {
                    $sponsor.Add("sponsor_id",(new-guid))
                }

                if(-not ((-not $sponsor.ContainsKey("image")) -and (-not $sponsor.ContainsKey("link")) -and (-not $sponsor.ContainsKey("width")) -and (-not $sponsor.ContainsKey("height")))) {

                    $dt = ([pscustomobject]$sponsor) | ConvertTo-DbaDataTable
                    
                    $filteredColMap = @{}

                    $theKeys = $sponsorColMap.Keys | Where-Object {$sponsor.ContainsKey($_)}
                    $theKeys | % {$filteredColMap.Add($_,$sponsorColMap[$_])}

                    #$dt
                    try {
                    $dt | Write-DbaDataTable -SqlInstance "localhost" -Database "datasaturdays" -Table "sponsors" -ColumnMap $filteredColMap -EnableException
                    }
                    catch {
                        $_
                        $currentFile
                    }
                }
            }
        }
    }

}


# Fix sposnsors

$sql ="
UPDATE s SET link_url = NULL
FROM Sponsors AS s
WHERE link_url NOT LIKE 'http%';

"
Invoke-DbaQuery -SqlInstance "localhost" -Database "datasaturdays" -Query $sql



#----------------------- Milestones

$sql = "
    DECLARE @event_id uniqueidentifier;
    SELECT  @event_id = [event_id] FROM [dbo].[Events] WHERE name = 'Data Saturday #35 - Oslo'
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 1, 'Call for speakers open', NULL, 'January 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 2, 'Registration opens', NULL, 'January 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 3, 'Pre-conference Call for Speakers closes', NULL, '<strong>February 28th, 2023</strong>')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 4, 'Pre-conference sessions announced', NULL, '<strong>Late March, 2023</strong>')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 5, 'Call for speakers closes', NULL, '<strong>March 31st, 2023</strong>')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 6, 'Session selection, notifying speakers', NULL, 'April, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 7, 'Speakers and sessions published', NULL, 'May, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 8, 'Schedule published', NULL, '<strong>May, 2023</strong>')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 9, 'Pre-conference sessions', NULL, 'September 1st, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 10, 'Main conference event', NULL, 'September 2nd, 2023')        
    "   

Invoke-DbaQuery -SqlInstance "localhost" -Database "datasaturdays" -Query $sql


$sql = "
    DECLARE @event_id uniqueidentifier;
    SELECT  @event_id = [event_id] FROM [dbo].[Events] WHERE name = 'Data Saturday  #41 - Data Saturday Brisbane 2023'
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 1, 'Call for speakers open', NULL, 'May 22nd, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 2, 'Registration opens', NULL, 'May 30th, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 3, 'Call for speakers closes', NULL, '<strong>July 14th, 2023</strong>')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 4, 'Session selection, notifying speakers', NULL, 'July, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 5, 'Speakers and sessions published', NULL, 'August, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 6, 'Schedule published', NULL, 'August, 2023')
    INSERT INTO Milestones ( [milestone_id],[event_id],[order],[name],[milestone_date],[milestone_date_text]) 
    VALUES ('$(New-Guid)', @event_id , 7, 'Main conference event', NULL, 'September 30th, 2023')
    "

Invoke-DbaQuery -SqlInstance "localhost" -Database "datasaturdays" -Query $sql