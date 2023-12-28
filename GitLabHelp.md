## Connecting GitHub Desktop to GitLab.MSU.edu Projects
1. Download GitHub Desktop: [**https://desktop.github.com/**](https://desktop.github.com/)
2. Sign in using whatever GitHub account you prefer to use for the class. You can't sign in with your GitLab account, this is a GitHub application. Linking the two comes after. If you don't have a GitHub account, you can make one for free [**here**](https://docs.github.com/en/get-started/signing-up-for-github/signing-up-for-a-new-github-account) by clicking the **Sign up** button at the top and following the directions.
3. Login to [**GitLab.msu.edu**](http://gitlab.msu.edu) on a web browser.
4. Click **'Preferences'** under your profile picture (which is in the top-right corner of the screen).
5. On the lefthand sidebar, click **'Access Tokens'**
	1. Type in a **Token name** (it can be anything).
	2. Leave the **Expiration date** blank
	3. Under **Select scopes** select only **'api'**.
	4. Click **Create personal access token**.
6. You'll get a popup telling you the token ID. **_You ONLY get to see this token ID once. Copy and paste it somewhere or WRITE IT DOWN!!_**
7. **To clone a project into GitHub Desktop:**
	1. Go to the main GitLab repository page (e.g., gitlab.msu.edu/mi482-s24/[your name]/[project name]) and click **'Clone'**.
	2. Click the clipboard copy icon next to **'Clone with HTTPS'** to copy the link.
	3. Return to **GitHub Desktop**. From the **File** menu, choose **'Clone Repository** or click the **'Clone Repository'** button.
	4. Click the **URL** tab.
	5. Input the HTTPS link that you just copied as the Repository URL.
	6. Choose whatever local path you want.
	7. Click **'Clone'**.
	8. It's going to prompt you for login information.
		* For username, use your GitLab username (e.g., fakeSampleStudent@msu.edu).
		* For the passcode/password/auth token, use the copied token from *step 6*. 
	9. You should be good to go! 