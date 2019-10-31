
# Google OAuth authorization_code grant server side flow:

Step 1 : Authorize and show consent 
Redirect URL will be passed along with client_id registered.

HTTP/1.1 302 Found
Location: https://accounts.google.com/o/oauth2/v2/auth?redirect_uri=https%3A%2F%2Fdevelopers.google.com%2Foauthplayground&prompt=consent&response_type=code&client_id=84000154995-gnbo7ij17rvcc4j85132cfdjfrub8p5v.apps.googleusercontent.com&scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Ftasks+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Ftasks.readonly&access_type=offline

Step 2 : Return the access code back to redirect URL

GET /oauthplayground/?code=4/sgHxc02FK4sl1ofq3iJJf0653e5lUx5ZgnzpdOilNH5NG4vM7_Acdh9iQd1jap70NrJCS4kv-ydQ-rbztcvN0j8&scope=https://www.googleapis.com/auth/tasks.readonly%20https://www.googleapis.com/auth/tasks HTTP/1.1
Host: developers.google.com


Step 3 : Exchange code for access token 

Post to token endpoint along with client credential.( {NEEDS_UPDATE} should be replace by your client scerect)


POST /oauth2/v4/token HTTP/1.1
Host: www.googleapis.com
Content-length: 321
content-type: application/x-www-form-urlencoded
user-agent: google-oauth-playground
code=4%2FsgHxc02FK4sl1ofq3iJJf0653e5lUx5ZgnzpdOilNH5NG4vM7_Acdh9iQd1jap70NrJCS4kv-ydQ-rbztcvN0j8&redirect_uri=https%3A%2F%2Fdevelopers.google.com%2Foauthplayground&client_id=84000154995-gnbo7ij17rvcc4j85132cfdjfrub8p5v.apps.googleusercontent.com&client_secret={NEEDS_UPDATE}&scope=&grant_type=authorization_code


Get the response :
`
{
  "access_token": "ya29.Il-pBwwEduTS2-_oMNkZ6B5Tdybg3DxfePMIWPaKXSwL63ZZZ-oVc6GD9yc7o93sY9ZPnAlbHJSP94Xyw-yxKy_GrVyVzoIHiDSmW1qQLaqAhzDeuWNLVPxDUKE8h8IL4w", 
  "scope": "https://www.googleapis.com/auth/tasks.readonly https://www.googleapis.com/auth/tasks", 
  "token_type": "Bearer", 
  "expires_in": 3600, 
  "refresh_token": "1//04xnt-Juij5aeCgYIARAAGAQSNwF-L9Ircefryit9f7pGs5SFlCnhTmT3l2aBxeRcmN_Ab0mSMSxNuW5VPNH6H1cFJo7uRb6x8a"
}
`

Step 4 : Acceess api by passing token 
`
POST /tasks/v1/lists/MTQzNzc2OTUwNDMyNTE3NjgyOTk6MDow/tasks HTTP/1.1
Host: www.googleapis.com
Content-length: 29
Content-type: application/json
Authorization: Bearer ya29.Il-pBwwEduTS2-_oMNkZ6B5Tdybg3DxfePMIWPaKXSwL63ZZZ-oVc6GD9yc7o93sY9ZPnAlbHJSP94Xyw-yxKy_GrVyVzoIHiDSmW1qQLaqAhzDeuWNLVPxDUKE8h8IL4w
{
"title":"From playground"
}
`



