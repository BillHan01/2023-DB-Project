# 数据库课程设计项目 Git 使用手册

*Creator: Han Jiarui*

*Updated on: 2023/5/22*

# 1 GitHub 使用指南

## 1.1 仓库

仓库（repository）通常用于组织单个项目。 可以包含文件夹和文件、图像、视频、电子表格和数据集 - 项目所需的任何内容。 通常，存储库包括一个 README 文件，其中具有项目的相关信息。 README 文件以纯文本 Markdown 语言编写。使用 GitHub 可在新建存储库的同时添加 README 文件。 GitHub 还提供了其他常用选项，例如许可证文件。

## 1.2 分支

通过分支（branch），您可以同时拥有不同版本的仓库。

默认情况下，存储库有一个名为 `main` 的分支，它被视为最终分支。 可在存储库中从 `main` 创建其他分支。 您可以使用分支一次拥有项目的不同版本。 当您想要在不更改主要代码源的情况下向项目添加新功能时，这非常有用。 在合并主分支之前，在不同分支上完成的工作不会显示在主分支上。 可使用分支进行试验和编辑，然后再将其提交到 `main`。

从 `main` 分支创建分支时，创建的是 `main` 在当时的副本或快照。 如果其他人在你处理分支时对 `main` 分支进行了更改，你可拉取这些更新。

### 1.2.1 创建分支

1. 单击 `hello-world` 存储库的“Code”选项卡。
2. 在文件列表上方，单击“main”下拉菜单。
    
    ![https://docs.github.com/assets/cb-78797/images/help/branches/branch-selection-dropdown.png](https://docs.github.com/assets/cb-78797/images/help/branches/branch-selection-dropdown.png)
    
3. 在文本框中键入分支名称 `readme-edits`。
4. 单击“创建分支: 从 main 创建 readme-edits”。
    
    现在你有两个分支：`main` 和 `readme-edits`。 现在，它们看起来完全相同。 接下来，您将向新分支添加更改。
    
    ![https://docs.github.com/assets/cb-59539/images/help/repository/new-branch.png](https://docs.github.com/assets/cb-59539/images/help/repository/new-branch.png)
    

### 1.2.2 创建与提交更改（Commit changes）

在上一步中创建新分支时，GitHub 会将你转到作为 `main` 副本的新 `readme-edits` 分支的代码页。

您可以对存储库中的文件进行更改并保存更改。 在 GitHub 上，保存的更改称为提交。 每个提交都有一个关联的提交消息，该消息是解释为什么进行特定更改的说明。 提交消息会捕获您更改的历史记录，以便其他参与者可以了解您执行了哪些操作及其原因。

1. 在你创建的 `readme-edits` 分支下，单击 README.md 文件。
2. 若要编辑文件，请单击 。
3. 在编辑器中，编写一些关于您自己的内容。 尝试使用不同的 Markdown 元素。
4. 单击“Commit changes...”。
5. 在“Commit changes”框中，编写描述更改的提交消息。
6. 单击“Commit changes”。

这些更改将仅适用于 `readme-edits` 分支上的 README 文件，所以此分支现包含 `main` 中没有的内容。

### 1.2.3 拉取请求（Pull request）

现在你已在从 `main` 创建的分支 `readme-edits` 中进行了更改，接下来可打开拉取请求。

拉取请求是 GitHub 上协作的核心。 打开拉取请求后，可以提出更改，要求某人审查和提取您的贡献并将其合并到其分支中。 拉取请求显示两个分支中内容的差异。 变化、增减以不同的颜色显示。

只要进行提交，便可打开拉取请求并开始讨论，即使在代码完成之前亦可。

通过在拉取请求消息中使用 GitHub 的 `@mention` 功能，可向特定人员或团队请求反馈。

您甚至可以在自己的存储库中打开拉取请求并自行合并。 这是在处理大型项目之前了解 GitHub 流程的好方法。

1. 单击 `hello-world` 存储库的“Pull request”选项卡。
2. 单击“New pull request”
3. 在“示例比较”框中，选择你创建的分支 `readme-edits`，与 `main`（原始分支）进行比较。
4. 在 Compare（比较）页面上的差异中查看您的更改，确保它们是您要提交的内容。
    
    ![https://docs.github.com/assets/cb-32937/images/help/repository/diffs.png](https://docs.github.com/assets/cb-32937/images/help/repository/diffs.png)
    
5. 单击“Create pull request”****。
6. 为拉取请求指定一个标题，并写下更改的简要说明。 您可以包含表情符号以及拖放图像和 gif。
7. （可选）在标题和说明右侧，单击“审阅者”旁边的 。 单击“代理人”、“标签”、“项目”或“里程碑”，将这些选项的任何一个添加到拉取请求 。 您不需要添加任何内容，但这些选项提供了使用拉取请求进行协作的不同方式。 有关详细信息，请参阅“[关于拉取请求](https://docs.github.com/zh/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests)”。
8. 单击“创建拉取请求”****。

您的协作者现在可以查看您的编辑内容并提出建议。

### 1.2.4 合并拉取请求（Merge pull request）

在最后一步中，你要将 `readme-edits` 分支合并到 `main` 分支中。 合并拉取请求后，`readme-edits` 分支上的更改将合并到 `main`。

有时，拉取请求可能会引入与 `main` 上的现有代码冲突的代码更改。 如果存在任何冲突， GitHub 将提醒您有关冲突代码的信息，并防止合并，直到冲突解决为止。 您可以进行解决冲突的提交，也可以使用拉取请求中的注释与团队成员讨论冲突。

在您已准备好将分支合并到主分支中后：

1. 在拉取请求底部，单击“Merge pull request”以将更改合并到 `main` 中。
2. 单击“Confirm merge”。 您将收到一条消息，指出请求已成功合并且请求已关闭。
3. 单击“Delete branch”。 现在你的拉取请求已合并，并且你的更改位于 `main` 上，接下来你可安全地删除 `readme-edits` 分支。 如果要对项目进行更多更改，可以随时创建新分支并重复此过程。

# 2.Git 功能使用

GitHub 的核心是名为 Git 的开源版本控制系统 (VCS)。 Git 负责在你计算机上本地发生的、与 GitHub 有关的所有内容。

## 2.1 Git 的3种使用方式

如果无需在本地使用文件，GitHub 可让你在浏览器中直接完成许多 Git 相关的操作，包括：

- [创建存储库](https://docs.github.com/zh/get-started/quickstart/create-a-repo)
- [存储库创建分支](https://docs.github.com/zh/get-started/quickstart/fork-a-repo)
- [管理文件](https://docs.github.com/zh/repositories/working-with-files/managing-files)
- [社交化](https://docs.github.com/zh/get-started/quickstart/be-social)

若要在命令行中使用 Git，你将需要在计算机上下载、安装和配置 Git。 您还可以安装 GitHub CLI 以从命令行使用 GitHub 。 有关详细信息，请参阅“[关于 GitHub CLI](https://docs.github.com/zh/github-cli/github-cli/about-github-cli)”。

如果要在本地使用 Git，但不想使用命令行，可以下载并安装 [GitHub Desktop](https://desktop.github.com/) 客户端。 有关详细信息，请参阅“[安装和配置 GitHub Desktop](https://docs.github.com/zh/desktop/installing-and-configuring-github-desktop)”。

## 2.2 基本的 Git 命令

为使用 Git，开发人员使用特定命令来复制、创建、更改和合并代码。 这些命令可以直接从命令行执行，也可以使用 GitHub Desktop等应用程序执行。 以下是使用 Git 的一些常用命令：

- `git init` 初始化一个全新的 Git 存储库并开始跟踪现有目录。 它在现有目录中添加一个隐藏的子文件夹，该子文件夹包含版本控制所需的内部数据结构。
- `git clone` 创建远程已存在的项目的本地副本。 克隆包括项目的所有文件、历史记录和分支。
- `git add` 暂存更改。 Git 跟踪对开发人员代码库的更改，但有必要暂存更改并拍摄更改的快照，以将其包含在项目的历史记录中。 此命令执行暂存，即该两步过程的第一部分。 暂存的任何更改都将成为下一个快照的一部分，并成为项目历史记录的一部分。 通过单独暂存和提交，开发人员可以完全控制其项目的历史记录，而无需更改其编码和工作方式。
- `git commit` 将快照保存到项目历史记录中并完成更改跟踪过程。 简言之，提交就像拍照一样。 任何使用 `git add` 暂存的内容都将成为使用 `git commit` 的快照的一部分。
- `git status` 将更改的状态显示为未跟踪、已修改或已暂存。
- `git branch` 显示正在本地处理的分支。
- `git merge` 将开发线合并在一起。 此命令通常用于合并在两个不同分支上所做的更改。 例如，当开发人员想要将功能分支中的更改合并到主分支以进行部署时，他们会合并。
- `git pull` 使用远程对应项的更新来更新本地开发线。 如果队友已向远程上的分支进行了提交，并且他们希望将这些更改反映到其本地环境中，则开发人员将使用此命令。
- `git push` 使用本地对分支所做的任何提交来更新远程存储库。

有关详细信息，请参阅 [Git 命令的完整参考指南](https://git-scm.com/docs)。

# 3 GitHub Desktop 使用指南

## 3.1 克隆仓库

您可以使用 GitHub 将远程仓库克隆到 GitHub Desktop。

1. 开始克隆前，请先登录到 GitHub.com 和 GitHub Desktop。
2. 在 GitHub.com 上，导航到存储库的主页。在文件列表上方，单击 “Code”。
    
    ![https://docs.github.com/assets/cb-32892/images/help/repository/code-button.png](https://docs.github.com/assets/cb-32892/images/help/repository/code-button.png)
    
3. 若要使用 GitHub Desktop 克隆并打开存储库，请单击 “Open with GitHub Desktop”。
    
    ![https://docs.github.com/assets/cb-44807/images/help/repository/open-with-desktop.png](https://docs.github.com/assets/cb-44807/images/help/repository/open-with-desktop.png)
    
4. 单击“Choose...”并找到要克隆存储库的本地目录。
    
    ![https://docs.github.com/assets/cb-34382/images/help/desktop/clone-choose-button-url-mac.png](https://docs.github.com/assets/cb-34382/images/help/desktop/clone-choose-button-url-mac.png)
    
    注意：如果存储库配置为使用 LFS，系统会提示你初始化 Git LFS。
    
5. 单击“Clone”。

## 3.2 **管理分支**

### 3.2.1 创建分支

1. 在应用顶部，单击 “Current Branch”，然后在分支列表中单击要将新分支作为基础的分支。
    
    ![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)
    
2. 单击“New Branch”。
    
    ![https://docs.github.com/assets/cb-26429/images/help/desktop/new-branch-button-mac.png](https://docs.github.com/assets/cb-26429/images/help/desktop/new-branch-button-mac.png)
    
3. 在“Create Branch”窗口的“名称”下，键入新分支的名称。 在“基于以下内容创建分支...”下，为新分支选择一个基础分支。 单击“Create Branch”。

### 3.2.2 从以前的提交创建分支

1. 在左边栏中，单击“History”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
2. 右键单击要从中创建新分支的提交，并选择“从提交创建分支”。
    
    ![https://docs.github.com/assets/cb-141531/images/help/desktop/create-branch-from-commit-context-menu.png](https://docs.github.com/assets/cb-141531/images/help/desktop/create-branch-from-commit-context-menu.png)
    
3. 在“创建分支”窗口的“名称”下，键入新分支的名称。单击“创建分支”。

### 3.2.3 发布分支

在GitHub Desktop上创建完分支后，还需要发布分支以便其可用于在 GitHub 上进行协作。

1. 在存储库栏，单击 “当前分支”，然后单击要发布的分支。
    
    ![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)
    
2. 单击“发布分支”。
    
    ![https://docs.github.com/assets/cb-31324/images/help/desktop/publish-branch-button.png](https://docs.github.com/assets/cb-31324/images/help/desktop/publish-branch-button.png)
    

### 3.2.4 在分支间切换

您可以查看并提交到任何仓库的分支。 **如有未提交但已保存的更改，您需要决定如何处理更改，然后才可切换分支。** 您可以在当前分支上提交更改、隐藏更改以临时将它们保存在当前分支上，或者将更改传送到新分支。 

1. 在存储库栏，单击 “当前分支”，然后单击要切换的分支。
    
    ![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)
    
2. 如果已保存、未提交的更改，请在“切换分支”窗口中，选择“在 CURRENT-BRANCH 上保留我的更改”或“将我的更改引入 NEW-BRANCH”，然后单击“切换分支”。下一次切回来的时候可以在被隐藏的更改中找到未提交的更改。

### 3.2.5 删除分支

请注意，**您不能撤消对分支的删除。**

1. 在存储库栏，单击 “当前分支”，然后单击要删除的分支。
2. 在菜单栏中，单击“分支”，然后单击“删除...” 。还可按 Ctrl+Shift+D。

![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)

![https://docs.github.com/assets/cb-28505/images/help/desktop/delete-branch-win.png](https://docs.github.com/assets/cb-28505/images/help/desktop/delete-branch-win.png)

### 3.2.6 同步分支

您可以通过拉取自上次同步以来在 GitHub 上添加到分支的任何提交来同步本地分支与远程仓库。 如果从其他设备进行提交，或有多人参与项目，则需要同步本地分支以保持分支的更新。

当您拉取到本地分支时，只会更新仓库的本地副本。 

1. 在 GitHub Desktop 中，使用 “当前分支”下拉列表，然后选择要更新的本地分支。
2. 若要检查远程分支上的提交，请单击“提取源”
    
    ![https://docs.github.com/assets/cb-58342/images/help/desktop/fetch-button.png](https://docs.github.com/assets/cb-58342/images/help/desktop/fetch-button.png)
    
3. 若要从远程分支拉取任何提交，请单击“拉取源”或“具有变基的拉取源” 。
    
    ![https://docs.github.com/assets/cb-24440/images/help/desktop/pull-button.png](https://docs.github.com/assets/cb-24440/images/help/desktop/pull-button.png)
    
4. 使用文本编辑器、命令行或其他工具（根据您的偏好）解决任何合并冲突。 有关详细信息，请参阅“[解决合并冲突](https://docs.github.com/zh/pull-requests/collaborating-with-pull-requests/addressing-merge-conflicts)”。

## 3.3 管理更改

### 3.3.1 显示差异

您可以更改差异在 GitHub Desktop 中的显示方式，以适应您的审核需求。

要更改您查看差异的方式，请单击差异视图右上角的设置按钮。

- 若要更改显示整个差异的方式，请在“差异显示”下选择“统一”或“拆分” 。 统一视图线性显示变化，而分割视图则在左侧显示旧内容，在右侧显示新内容。
- 要隐藏空格更改以便专注于更实质性的更改，请选择“隐藏空格更改”。

![https://docs.github.com/assets/cb-118206/images/help/desktop/diff-selection.png](https://docs.github.com/assets/cb-118206/images/help/desktop/diff-selection.png)

如果您需要查看的文件比默认情况下显示的 GitHub Desktop 更多，则可以扩展差异。

- 要查看突出显示的更改的上下几行，请单击行号上方或下方的箭头。
- 要查看整个文件，请在差异视图中右键单击，然后单击“扩展整个文件”。

![https://docs.github.com/assets/cb-71541/images/help/desktop/expand-diff-view.png](https://docs.github.com/assets/cb-71541/images/help/desktop/expand-diff-view.png)

### 3.3.2 选择更改内容

在文本编辑器中更改文件并本地保存后，您会在 GitHub Desktop 中看到更改。

在左侧边栏中的“更改”选项卡中：

- 红色的  图标表示删除的文件。
- 黄色的  图标表示经过修改的文件。
- 绿色的  图标表示添加的文件。
- 要访问暂存的更改，请单击“暂存的更改”。
- 要将“所有文件中的所有更改”添加到单个提交中，请保留所选列表顶部的复选框处于选中状态。
    
    ![https://docs.github.com/assets/cb-16555/images/help/desktop/commit-all.png](https://docs.github.com/assets/cb-16555/images/help/desktop/commit-all.png)
    
- 要将“一个或多个文件中的所有更改”添加到单个提交中，请取消选中不想包含的文件旁边的复选框，只保留要包含在提交中的文件。 在选择文件后使用 `Spacebar` 或 `Enter` 键可切换复选框状态。

**创建部分提交**

如果一个文件包含多处更改，但只有部分更改要包含在提交中，则可创建部分提交。 其余更改会保持不动，以便您进行其他修改和提交。 这允许您进行单独、有意义的提交，例如使提交中的换行符更改区别于代码或文字更改。

要从提交中排除更改的行，请单击一条或多条更改的行，以使蓝色消失。 仍以蓝色突出显示的行将包含在提交中。

![https://docs.github.com/assets/cb-41593/images/help/desktop/partial-commit.png](https://docs.github.com/assets/cb-41593/images/help/desktop/partial-commit.png)

### 3.3.3 放弃更改

如果您有不想保留的未提交更改，可以放弃这些更改。 这将从计算机上的文件中删除更改。 您可以放弃一个或多个文件中所有未提交的更改，也可以丢弃添加的特定行。

**丢弃一个或多个文件中的更改**

1. 在已更改文件的列表中，选择要放弃自上次提交后更改的文件。 要选择多个文件，请单击 `shift` 并单击要放弃更改的文件范围。 1. 单击“放弃更改”或“放弃所选更改”以放弃对一个或多个文件的更改，或单击“放弃所有更改”以放弃自上次提交后对所有文件的更改 。
    
    ![https://docs.github.com/assets/cb-152195/images/help/desktop/discard-changes-mac.png](https://docs.github.com/assets/cb-152195/images/help/desktop/discard-changes-mac.png)
    
2. 若要确认更改，请在“确认放弃更改”窗口中查看受影响的文件，然后单击“放弃更改”。

**丢弃一行或多行中的更改**

您可以丢弃一个或多个未提交的已更改行。

**注意：** 在添加和删除行的一组更改中不可放弃单行。

要放弃一个已添加的行，请在已更改的行列表中，右键单击要放弃的行的行号，然后选择“放弃添加的行”。

![https://docs.github.com/assets/cb-69248/images/help/desktop/discard-single-line.png](https://docs.github.com/assets/cb-69248/images/help/desktop/discard-single-line.png)

要放弃一组更改的行，请右键单击要放弃的行的行号右侧的垂直条，然后选择“放弃已添加的行”。

![https://docs.github.com/assets/cb-44904/images/help/desktop/discard-multiple-lines.png](https://docs.github.com/assets/cb-44904/images/help/desktop/discard-multiple-lines.png)

### 3.3.4 推送更改

对选择要包含在提交中的更改感到满意后，编写提交消息并推送更改。 如果协作处理了某个提交，也可以将提交归于多个作者。

注意：默认情况下，GitHub Desktop 会将您创建的标记推送到含相关提交的仓库。 

1. 在更改列表底部的 Summary（摘要）字段中，键入一条简短、有意义的提交消息。 您也可以在 Description（说明）字段中添加关于更改的更多信息（可选） 。
    
    ![https://docs.github.com/assets/cb-82057/images/help/desktop/commit-message.png](https://docs.github.com/assets/cb-82057/images/help/desktop/commit-message.png)
    
2. （可选）要将某个提交归于另一个作者，请单击合作作者图标并输入要包含的用户名。
    
    ![https://docs.github.com/assets/cb-16103/images/help/desktop/add-co-author-commit.png](https://docs.github.com/assets/cb-16103/images/help/desktop/add-co-author-commit.png)
    
3. 在“说明”字段下，单击“提交到分支”。
4. 如果您尝试提交的分支受保护，Desktop 将警告您。
    - 若要移动更改，请单击“切换分支”。
    - 若要将更改提交到受保护的分支，请单击“提交到 BRANCH”。
    
    有关受保护分支的详细信息，请参阅“[关于受保护分支](https://docs.github.com/zh/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches)”。
    
5. 若要将本地更改推送到远程存储库，请在存储库栏中单击“推送源”。
    
    ![https://docs.github.com/assets/cb-17787/images/help/desktop/push-to-origin.png](https://docs.github.com/assets/cb-17787/images/help/desktop/push-to-origin.png)
    
6. 如果你有基于所处理的分支的拉取请求，GitHub Desktop 将在存储库栏的“Current Branch”部分旁显示已针对拉取请求运行的检查的状态。 有关检查的详细信息，请参阅“[在 GitHub Desktop 中查看和重新运行检查](https://docs.github.com/zh/desktop/contributing-and-collaborating-using-github-desktop/working-with-your-remote-repository-on-github-or-github-enterprise/viewing-and-re-running-checks-in-github-desktop)”。
    
    如果尚未为当前分支创建拉取请求，则 GitHub Desktop 将提供预览更改和创建请求的选项。 有关详细信息，请参阅“[从 GitHub Desktop 创建问题或拉取请求](https://docs.github.com/zh/desktop/contributing-and-collaborating-using-github-desktop/working-with-your-remote-repository-on-github-or-github-enterprise/creating-an-issue-or-pull-request-from-github-desktop)”。
    
    ![https://docs.github.com/assets/cb-49111/images/help/desktop/mac-preview-pull-request.png](https://docs.github.com/assets/cb-49111/images/help/desktop/mac-preview-pull-request.png)
    

### 3.3.5 隐藏更改

要将更改应用到仓库，您必须保存文件，然后将更改提交到分支。 如果您保存了尚未准备好提交的更改，可以隐藏更改供以后使用。 当您隐藏更改时，更改会暂时从文件中删除，然后您可以选择恢复或丢弃更改。 使用 GitHub Desktop 只能隐藏一组更改。 如果您使用 GitHub Desktop 来隐藏更改，则所有未保存的更改都将被隐藏。 在隐藏分支上的更改后，您可以安全地更改分支或对当前分支进行其他更改。

如果您在已保存但未提交更改时使用 GitHub Desktop 切换分支，则 GitHub Desktop 会提示您隐藏更改或将其带到其他分支。

**隐藏更改**

右键单击“更改的文件”标头。单击“隐藏所有更改”。

![https://docs.github.com/assets/cb-67885/images/help/desktop/mac-stash-all-changes.png](https://docs.github.com/assets/cb-67885/images/help/desktop/mac-stash-all-changes.png)

**恢复隐藏的更改**

如果尚未在隐藏更改的分支上，请单击存储库栏中的“ 当前分支”，然后单击带有储藏更改的分支。

![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)

在左侧边栏的“更改”选项卡中，单击“隐藏的更改”。

![https://docs.github.com/assets/cb-50782/images/help/desktop/stashed-changes-sidebar.png](https://docs.github.com/assets/cb-50782/images/help/desktop/stashed-changes-sidebar.png)

在边栏右侧的“储藏更改”下，单击“还原”。

**丢弃隐藏的更改**

如果尚未在隐藏更改的分支上，请单击存储库栏中的“ 当前分支”，然后单击带有储藏更改的分支。

在左侧边栏的“更改”选项卡中，单击“隐藏的更改”。

![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)

在边栏右侧的“储藏更改”下，单击“放弃”。

![https://docs.github.com/assets/cb-50782/images/help/desktop/stashed-changes-sidebar.png](https://docs.github.com/assets/cb-50782/images/help/desktop/stashed-changes-sidebar.png)

### 3.3.6 查看分支历史记录

每个提交会显示：

- 提交消息
- 提交创建的时间
- 提交者的用户名和头像（如果有）
- 提交的 SHA-1 哈希（唯一 ID）
1. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
2. 在“历史记录”选项卡上，单击要查看的提交。
    
    还可以使用 Ctrl 或 Shift 选择一系列多个连续提交。
    
    ![https://docs.github.com/assets/cb-248544/images/help/desktop/branch-history-commit.png](https://docs.github.com/assets/cb-248544/images/help/desktop/branch-history-commit.png)
    
3. 如果提交或提交范围中有多个文件，单击各个文件可查看对该文件的更改。
    
    ![https://docs.github.com/assets/cb-119769/images/help/desktop/branch-history-file.png](https://docs.github.com/assets/cb-119769/images/help/desktop/branch-history-file.png)
    

## 3.4 管理提交

### 3.4.1 还原提交

在还原到上一个提交时，还原本身也是提交。 原提交仍会保留在仓库的历史记录中。

提示：在还原多个提交时，最好按照从最新到最旧的顺序还原。 如果以其他顺序还原提交，可能会出现合并冲突。

在左边栏中，单击“历史记录”。

右键单击要还原的提交，然后单击“还原提交中的更改”。

![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)

![https://docs.github.com/assets/cb-146981/images/help/desktop/commit-revert-mac.png](https://docs.github.com/assets/cb-146981/images/help/desktop/commit-revert-mac.png)

### 3.4.2 挑拣特定提交

你可以使用 GitHub Desktop 在一个分支上选择特定的提交，并将提交副本复制到另一个分支。

1. 在 GitHub Desktop 中，单击“  当前分支”。
    
    ![https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png](https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png)
    
2. 在分支列表中，单击包含您想要挑拣的提交的分支。
    
    ![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)
    
3. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
4. 从“历史记录”选项卡将要挑拣的提交拖到 “当前分支”下拉菜单，然后将提交放在要将提交复制到的分支上。
    
    ![https://docs.github.com/assets/cb-112235/images/help/desktop/cherry-picking.png](https://docs.github.com/assets/cb-112235/images/help/desktop/cherry-picking.png)
    

### 3.4.3 提交重排序

1. 在 GitHub Desktop 中，单击“  当前分支”。
    
    ![https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png](https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png)
    
2. 在分支列表中，单击包含您想要重新排序其提交的分支。
    
    ![https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png](https://docs.github.com/assets/cb-38142/images/help/desktop/select-branch-from-dropdown.png)
    
3. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
4. 拖动您想要重新排序的提交，并将其置于两个相邻的提交之间。
    
    ![https://docs.github.com/assets/cb-56629/images/help/desktop/reorder-drag-and-drop.png](https://docs.github.com/assets/cb-56629/images/help/desktop/reorder-drag-and-drop.png)
    

当应用程序重新排序提交时，“正在进行重新排序”对话框将指示更改的进度。

注意，重新排序提交时，您可能会看到以下通知或错误消息。

- 通知指出，请求的分支更改将需要强制推送以更新远程分支。 当您重新排序的提交之前被推送到远程分支时，将会显示此通知。 强制推送会更改分支的提交历史记录，并影响处理该分支的其他协作者。 选择“开始重新排序”以开始重新排序，然后单击“强制推送源”以推送所做的更改 。
- 错误表示重排序失败，因为重新排序的提交之间有合并提交。
- 通知显示您当前分支上存在未提交的更改。 选择“”存储更改并继续，或选择“关闭”以关闭消息并提交更改 。 当不再有任何未提交的更改时，您可以重新排序您的提交。
- 消息指出，必须先解决冲突，然后应用程序才可继续重新排序分支上的提交。
    1. 单击“查看冲突”。
        
        ![https://docs.github.com/assets/cb-36254/images/help/desktop/reorder-resolve-conflicts.png](https://docs.github.com/assets/cb-36254/images/help/desktop/reorder-resolve-conflicts.png)
        
    2. 使用文本编辑器、命令行或其他工具（根据您的偏好）解决任何合并冲突。 有关详细信息，请参阅“[解决合并冲突](https://docs.github.com/zh/pull-requests/collaborating-with-pull-requests/addressing-merge-conflicts)”。
    3. 当所有冲突得到解决时，您可以重新排序您的提交。

### 3.4.4 压缩提交

压缩允许您将分支历史记录中的多个提交合并为单个提交。 这将有助于使您的仓库历史记录更易读和易懂。

1. 在 GitHub Desktop 中，单击“  当前分支”。
    
    ![https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png](https://docs.github.com/assets/cb-31388/images/help/desktop/current-branch-menu.png)
    
2. 在分支列表中，选择包含您要压缩的提交的分支。
3. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
4. 选择要压缩的提交，并将其放到要合并的提交上。
    
    可以选择一个提交，也可以使用 Ctrl 或 Shift 选择多个提交。
    
    ![https://docs.github.com/assets/cb-144288/images/help/desktop/squash-drag-and-drop.png](https://docs.github.com/assets/cb-144288/images/help/desktop/squash-drag-and-drop.png)
    
5. 修改新提交的提交消息。 要压缩的所选提交的提交消息已预先填充到“摘要”和“描述”字段中 。
6. 单击“压缩提交”。

注意，压缩提交时，您可能会看到以下通知或错误消息。

- 通知指出，请求的分支更改将需要强制推送以更新远程分支。 强制推送会更改分支的提交历史记录，并影响处理该分支的其他协作者。 选择“开始压缩”以开始压缩，然后单击“强制推送源”以推送所做的更改 。
- 错误表示压缩失败，因为重新排序的提交之间有合并提交。
- 通知显示您当前分支上存在未提交的更改。 选择“储藏更改并继续”以存储更改并继续，或选择“关闭”以关闭消息并提交更改 。 当不再有任何未提交的更改时，您可以压缩您的提交。

### 3.4.5 修改提交

修改提交是修改您当前分支中的最新提交的一种方式。 如果您需要编辑提交消息，或者忘记在提交中包含更改，这会很有帮助。

您可以继续修订提交，直到将其推送到远程仓库。 在推送提交后，修订选项在 GitHub Desktop 中禁用。 修订提交时，请将以前的提交替换为当前分支的新提交。 修订已推送到远程仓库的提交可能会对与仓库合作的其他协作者造成混淆。

1. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
2. 右键单击最新提交并选择“修改提交”。
    
    ![https://docs.github.com/assets/cb-151890/images/help/desktop/amend-commit-context-menu.png](https://docs.github.com/assets/cb-151890/images/help/desktop/amend-commit-context-menu.png)
    
3. 在“修改需要强制推送”对话框窗口中，单击“开始修改”。
4. 在“更改”选项卡中，使用“摘要”字段修改提交消息。 （可选）可以在“说明”字段中修改或添加关于提交的信息。
5. 选择您想要添加到提交的任何未提交更改。 有关选择更改的详细信息，请参阅“[在 GitHub Desktop 中提交并审查对项目的更改](https://docs.github.com/zh/desktop/contributing-and-collaborating-using-github-desktop/making-changes-in-a-branch/committing-and-reviewing-changes-to-your-project#selecting-changes-to-include-in-a-commit)”。
6. 完成更改后，单击“修改最后一次提交”。

### 3.4.6 管理标记

GitHub Desktop 可用于创建带注释的标记。 标记与提交相关，因此您可以使用标记在仓库的历史记录中标记单独的点，包括发行版的版本号。

默认情况下，GitHub Desktop 会将您创建的标记推送到含相关提交的仓库。

**创建标记：**

在左边栏中，单击“历史记录”。

1. 右键单击提交，然后单击“创建标记...”。

![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)

2. 在“创建标记”对话框窗口中，键入标记的名称。 单击“创建标记”。

![https://docs.github.com/assets/cb-152187/images/help/desktop/select-create-tag.png](https://docs.github.com/assets/cb-152187/images/help/desktop/select-create-tag.png)

**查看标记：**

1. 在左边栏中，单击“历史记录”。
    
    ![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)
    
2. 单击提交。
    
    注意：如果标记未被推送至远程存储库，GitHub Desktop 将显示箭头 。
    
    ![https://docs.github.com/assets/cb-87508/images/help/desktop/viewing-tags-in-history.png](https://docs.github.com/assets/cb-87508/images/help/desktop/viewing-tags-in-history.png)
    
3. 与提交相关的所有标记均在提交元数据中可见。
    
    ![https://docs.github.com/assets/cb-68111/images/help/desktop/viewing-tags-in-commit.png](https://docs.github.com/assets/cb-68111/images/help/desktop/viewing-tags-in-commit.png)
    

**删除标记：**

注意：你只能删除与尚未推送的提交关联的标记。

在左边栏中，单击“历史记录”。

右键单击提交。

![https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png](https://docs.github.com/assets/cb-130307/images/help/desktop/history-tab-in-commit-sidebar.png)

- 如果提交只有一个标记，单击“删除标记名称”。
    
    ![https://docs.github.com/assets/cb-189345/images/help/desktop/select-delete-tag.png](https://docs.github.com/assets/cb-189345/images/help/desktop/select-delete-tag.png)
    
- 如果提交有多个标记，将鼠标悬停在“删除标记...”，然后单击你要删除的标记。
    
    ![https://docs.github.com/assets/cb-204626/images/help/desktop/delete-tag-multiple.png](https://docs.github.com/assets/cb-204626/images/help/desktop/delete-tag-multiple.png)
    

# 4. Git 使用规范

本部分文档旨在为小组成员提供使用GitHub的规范和指导，以便更好地协作和管理项目。

**1. 仓库管理**

- 仓库应该设定合适的访问权限，确保只有授权的成员能够进行代码更改和提交。
- 创建清晰的分支结构，例如主分支（设置为`main`）和开发分支（例如`develop`），以支持并行开发和版本控制。

**2. 提交规范**

- 提交代码之前，务必先拉取最新的代码更新，确保代码库保持同步。
- 每次提交应该具有明确的目的和描述，遵循以下规范：
    - 提交消息应该简洁明了，概括提交的内容。
    - 遵循良好的提交消息格式，例如使用动词开头的简短描述，如"修复bug: ..."、"添加新功能: ..."。
    - 避免将多个无关的更改混合在一个提交中，每个提交应该只关注一个特定的功能或修复。
- 在提交之前进行代码审查，以确保代码质量和最佳实践。

**3. 分支管理**

- 开发新功能或修复问题时，应创建一个新的特性分支或修复分支。
- 保持分支结构的清晰和整洁，避免创建过多或过长时间未使用的分支。
- 当特性分支或修复分支完成时，应提交合并请求（Pull Request）到开发分支，并进行代码审查和讨论。
- 及时删除已合并的分支，以保持仓库的干净和易于维护。

**4. 代码审查**

- 代码审查是确保代码质量和一致性的重要步骤，每个提交应该进行代码审查。
- 在合并请求中请求其他成员进行代码审查，并及时响应审查意见和建议。
- 审查过程中，注意遵循最佳实践、代码风格和项目约定。

**5. Issue和任务管理**

- 使用GitHub的Issue功能来跟踪项目的问题、需求和任务。
- 在创建Issue时，提供清晰的描述和详细的信息，包括复现步骤、期望结果等。
- 将Issue与相关的提交和合并请求进行关联，以便进行跟踪和协作。

**6. 文档管理**

- 项目文档应该存储在仓库的 \docs 目录下，并遵循清晰统一的文档结构和格式。
- 使用README.md文件作为项目的主要文档入口，提供项目的简介、使用方法、依赖项等信息。
- 文档应该及时更新，以反映项目的最新状态和变更。

**7. 协作和沟通**

- 使用GitHub的讨论功能、评论和合并请求等工具进行协作和沟通。
- 及时回复评论和讨论，并保持适当的沟通和反馈。