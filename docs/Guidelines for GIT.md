#
# 🎯 Guidelines for Branch Name & Commit Messages
🔰 The below given are the guidelines to be followed while creating branch and committing changes in git.

#
## 🌠 Branch Naming Rules/Syntax

🔰 Branches allow you to work on different parts of a project without impacting the main branch.

🔰 When the work is complete, a branch can be merged with the main project.

🔰 You can even switch between branches and work on different projects without them interfering with each other.

- **_`main`_** or **_`master`_** - This is the default and usually considered the primary branch. It holds the stable and production-ready code.

- **_`develop`_** or **_`development`_** or **_`dev`_** - This branch acts as a staging area for features before they are merged into the main branch. It's particularly useful when multiple developers are working on various features simultaneously. It allows for integration testing and helps avoid directly merging features into the main branch.

🔰 The branches other than the above two, should be in the format: **_`BranchType-ModuleName-FeatureName`_**

🔰 Here the branchType represents what type of branch that you are creating.

🔰 The other types of branches are:

- **_`feature`_** or **_`feat`_**- Created when we are developing a new feature from the develop branch.

- **_`bugfix`_** or **_`fix`_** - Created when we need to make the minimum set of changes to a released build required to fix a bug.

- **_`improvement`_** - Created when we need to improve on the code quality, performance optimizations, Documentation Enhancements, User Interface Enhancements etc.

- **_`library`_** - It is rarely used in the case of Library Integration, Library Updates, Custom Modifications, Version Control.

- **_`prerelease`_** - It allows to publish releases with a pre-release version.

- **_`release`_** - Represent a complete feature set. (The only commits on the release branch are for bug fixes and important chores.)

- **_`hotfix`_** - Created when we need to quickly patch production releases.

      
For example:\
🔹 When we need to develop a new feature in the Banking module. So, the branch name will be **_`feature-BankApp-SavingsAccount`_** \
🔹 Suppose when we need to correct some bugs in the Banking module use - **_`bugfix-BankApp-FD`_**
  
#
## 🌠 Commit Message Format

🔰 Commit Message should have the format: \
**_`MessageType:description`_** or **_`MessageType : description`_** or **_`MessageType: description`_**

🔰 Here are some of the Semantic Commit Message Types,

- **_`feat`_**: New feature or functionality added

- **_`fix`_**: Bug fix 

- **_`docs`_**: Changes to the documentation

- **_`style`_**: Code style changes (formatting, missing semi colons, etc)

- **_`refactor`_**: Code refactor or restructuring without changing functionality (eg. renaming a variable)

- **_`test`_**:  Adding or modifying tests. (Adding missing tests, refactoring tests)

- **_`chore`_**: Maintenance tasks, build changes, or other non-functional changes (updating grunt tasks etc)

- **_`perf`_** : Indicate improvements made to the performance of the codebase.

- **_`ci`_**: When we make changes in the Continuous Integration, we use them.

- **_`build`_** : Refers to changes related to the build process of a software project like build optimizations etc.

- **_`revert`_** : Used when you want to revert the effects of one or more previous commits without deleting the commits themselves. 
    
For example:\
🔹 When we add authentication feature to a user method - **_`feat: add user authentication`_** \
🔹 When we change some documentation or work with the documentation of the methods - **_`docs: document user method`_**
