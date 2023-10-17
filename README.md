<body>
  <h1>Message from GitGuy [@tqoe]</h1>
  <p>Welcome to the project repository! This README will guide you through the process of using Git with Unity in our team environment. :smile: Don't worry, we're all learning, and it's really hard to break things. Message me on discord if you have any questions!</p>
<h1>In Unity</h1> 
<h2>Avoid Merge Conflicts!</h2>
<p>Merge conflicts happen when two people edit the same thing at the same time.</p>
<p>To make sure we don't cause many conflicts, make your own folder in Unity where you will work. Avoid editing stuff in other people's folders without permission. There are examples in the repo on how to name your folder.</p>
<p>Also, make your own scene to work in! You can copy paste someone else's to start off, just make sure you have your own scene</p>
<p>Finally, make prefabs for important stuff, and commit in git a lot!</p>
<h1>Git</h1>

<h2>Git GUI Apps</h2>
  <p>Using git with manual commands is more difficult than using an git on a GUI app, so I suggest using a GUI client. I personally use <a href="https://www.gitkraken.com/"><strong>GitKraken</strong></a> but you can use whatever you want. The guide below uses the git commands, but the process is basically the same for a GUI app, just easier and with pretty buttons.</p>

<h2>Cloning the Repo</h2>
<p>Once you've got your client, you're ready to clone the repo and make your branch! It's different for each GUI, but your program should have an option to clone. From there click the green [<> Code] button on this page (scroll up), copy the HTTPS link and follow the steps in your program to clone the repo.</p>

<h2>Main Branch Info</h2>
  <p>The <strong>main branch</strong> in this repo is protected and serves as the source of truth for our project. As the repository owner, I will be merging changes from your branches (more on that later) into the main branch manually. When you complete your work, try to merge the <strong>main branch</strong>  into your development branch (explained below), as long as there are no conflicts.</p>
  <h2>For Each Feature, Make a New Branch</h2>
  <p>To create a new branch for your development work, use the following command:</p>
  <pre><code>git checkout -b your_branch_name</code></pre>
  <p>[Or click the branch button on your GUI client]</p>

Replace <code>your_branch_name</code> with a descriptive name for your branch, usually the name of the feature you're adding. ex: PlayerMovement, CharacterArt, BurstWeapon</p>
<p>Once you've made your branch, go crazy with creating and adding stuff in Unity! In a GUI client, make sure the correct branch is selected before committing changes. 
 <h2>Committing Changes to Your Branch</h2>
  <p>When you have made changes to your Unity project, follow these steps to commit them to your branch:</p>
  <ol>
    <li>Stage the changes: <code>git add .</code></li>
    <li>Commit the changes with a descriptive message: <code>git commit -m "Your commit message"</code></li>
    <li>Push the changes to the remote repository: <code>git push origin your_branch_name</code></li>
  </ol>

  <p>[Or follow the same steps in your GUI]</p>
  <p>Name your commits based on what you chaged. ex: Added Enemy, Fixed Movement Bug, Added Enemy Art</p>

  <h2>[OPTIONAL] Merging Main Branch into Your Development Branch</h2>
  <p>Before your changes can make it into main, it's good to make sure your branch is up to date with the latest changes. Merge main into your branch, then test it in Unity to make sure everything works. This can't break main, it's just to make sure your changes are up to date with main. You can skip this step whole step if it's scary and I will do it for you. When you are ready to merge the main branch into your development branch, follow these steps:</p>
  <ol>
    <li>Make sure you are on your development branch: <code>git checkout your_branch_name</code></li>
    <li>Pull the latest changes from the main branch: <code>git pull origin main</code></li>
  </ol>
  <p>If there are no conflicts, your branch will be up to date with the main branch and therefore really easy to merge into main.</p>
  <h2>[OPTIONAL] Handling Merge Conflicts</h2>
  <p>Only do this if you want to learn git/challenge yourself! If you encounter merge conflicts, while merging the main branch into your development branch, you will need to resolve them manually. Open the conflicting files in a text editor and look for the conflict markers (<code>&lt;&lt;&lt;&lt;&lt;&lt;&lt;</code>, <code>=======</code>, and <code>&gt;&gt;&gt;&gt;&gt;&gt;&gt;</code>). Edit the file to resolve the conflicts, remove the conflict markers, and save the changes.</p>
  <p>After resolving all conflicts, stage the changes with <code>git add .</code>, commit them with <code>git commit</code>, and push the changes to the remote repository with <code>git push origin your_branch_name</code>.</p>
  <h2>Conclusion and Additional Resources</h2>
  <p>By following these instructions, you will be well-equipped to use Git with Unity in our team environment. </p>
</body>

</html>
