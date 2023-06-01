# GTAC-Tools

The non-CMS portion of the Greater Toledo Aquatic Club website. This was a major project that I undertook when the team's previous website provider went out of business and the competitor was multiple orders of magnitude more expensive. I taught myself ASP.NET in order to have a website that would replace all the features that we were using on the previous commercial site. 

All Features

Account
 - Change password
 - Create website account (with hashed and salted password)
 - Lost password handler
 - Create new account with the team (would send me a text on account creation)
 - Special page to create administrative accounts with different types of privileges


Team Administrator
 - Create and assign swimmers to training groups
 - Team setup page (global variables)
 - Team members view list
 - Contact file creator for integration with Google Contacts (so I would always know who was calling)
 - Set up a list of schools for autocomplete option when parents sign up (for marketing)

Coach
 - Attendance tracker which combined results from kids officially signed up, as well as kids that showed up on a trial basis so their attendance would automatically import when they did sign up (this feature is not available anywhere else commercially currently)
 - Ability to email group, either custom, or using templates with keywords for individualization
 - Meet event picking without having to have a different page for each swimmer (this feature is not available anywhere else commercially currently)
 - Time entry validated on frontend and backend (very complicated due to requirements specific to swimming, it does not work like normal time)

Carpool Map
 - Used the Google Maps API to filter members by group so members could set up car pools and would suggest carpools (this feature is not available anywhere else commercially currently)

Database Manager (my accound)
 - Primarily responsible for taking care of the data for the team
 - Ability to add an update meets to the competition schedule (using file provided from meet hosts)
 - Ability to edit sessions (usually provided automatically by a file from the meet hosts)
 - Ability to create events that require sign up that are not meets (banquets and parties)
 - Direct CRUD access on most database data
 - Team reports

Office Manager
 - Specific account for 1 person who acted on this role for our team
 - Ability to approve registered swimmers to actually be allowed onto the team
 - Banquet signups and setup
 - Reduced database CRUD abilities from the Database Manager 
 - Partial billing management (was about to start working on this before I left the team)
 - Ability to send urgent text messages to the team (practices changes due to weather mainly)
 - Ability to print certificates for swimmers who recently swam a time that placed on the team Top 10 record board
 - Set up volunteer jobs for team hosted meets

Parent
 - Calendar for all registered children including practice times and meets registered for
 - Ability to update personal information that would need special handling by USA Swimming
 - Attendance reports
 - Sign up swimmers for meets by session, as well as include notes for the coach
 - Sign up for volunteer jobs at team hosted meets.

Other details
 - 3 tiered data structure 
 - Was able to use and create files that were used by the desktop software that had a 99% market share at the time, especially important for use with Meet Management software
 - Used cutting edge technologies of the time, AJAX so a lot of the pagaes that used more concentration from the user did not require page reloading
 - Managed to update Microsoft automaticlly supplied code to be able to use batch inserts and batch updates for the coaches picking events. This cut time processing all the entries from several minutes on our previous website provider to less than .1 second. (This was only possible due to the new partial classes that had come out not long before.)




