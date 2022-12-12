# Git Workflow

- [Branching](#branching)
  - [Guidelines](#guidelines)
  - [Branch Naming](#branch-naming)
- [Commits](#commits)
- [Pull-Requests](#pull-requests)
- [Code Review Guidelines](#code-review-guidelines)
  - [Everyone](#everyone)
  - [Having Your Code Reviewed](#having-your-code-reviewed)
  - [Reviewing Code](#reviewing-code)

## Branching
Based on GitFlow [here](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) (optional).

We will use the following branches:

- `develop` - The main development branch. This is where most of the work is done.
- `production` - The main release branch. No commits directly to this branch!
- `feature/...` - Feature branches. Used when making additions or changes to the development branch.
- `fix/...` - Like a feature branch but for bug fixes.

### Guidelines

- **No one** is allowed to commit directly to `production`. All development is done in the `develop` branch.

- When making small single-commit changes, you may commit them directly to the `develop` branch. But when making larger changes you should start a feature branch. If you are making code changes, you should almost always create a feature branch for it, because otherwise code-review can't take place.

- Merging of feature branches is done via GitHub **pull-requests** which will need to pass **code-review**.
- When merging feature branches, **squash** before you merge (on GitHub you can do this by clicking the arrow next to the merge button and choosing squash and merge). This will help create a clean history and will give you a chance to re-summarize your changes. Read [Two years of squash merge](https://blog.dnsimple.com/2019/01/two-years-of-squash-merge/).

- Feature branches should be kept small and focused on a single feature or change. This will make for a cleaner history and make code-reviews easier. If you are building a large system over a longer period of time, it may *sometimes* be better to split the work up in several iterations (and feature branches).

### Branch Naming

- Branch names are specified in `lower-kebab-case`
- They take the form `{prefix}/branch-name`, eg. `feature/camera-shake`
- Feature branches are prefixed with `feature/`
- Bug fix branches are prefixed with `fix/`

## Commits

*The commit history of a project serves as a kind of documentation, and nobody likes bad documentation.*

- A commit should represent a single logical change. Consider splitting your work into multiple commits if needed.
- The subject line should summarize the changes as well as possible.
- Capitalize the subject line and do not end it with a period.
- Keep the subject line short - not much longer than *50 characters*. Use the body to describe your changes in more detail.
- Write everything in English.
- Use the [imperative mood](https://chris.beams.io/posts/git-commit/#imperative) in the subject line.
- Use the body to explain what and why, not how. (not always needed)
- You can optionally prefix your commit messages with the context of what part of the codebase/project the commit affects, eg. `Player: Fixed inconsistent jump height`. This can help improve the clarity of your commit messages in some cases.

Good article about writing good commit messages: https://chris.beams.io/posts/git-commit/

## Pull-Requests

- Naming your pull-requests is just as important as naming your [commits](#commits). The name helps others quickly identify what a pull-request does and makes the history cleaner.
- Writing a description helps the reviewer understand what changes are made in the pull-request. It is also valuable if you ever need to go back to an old pull-request to see what changes were made. It can be a good idea to link or reference the corresponding task on Hack 'n Plan as it likely describes the task quite well.
- Be sure to include any known issues/flaws or things that are not finished yet in the description. This will save both you and the reviewer time.
- A pull-request must be reviewed and approved by at least one person be merged.
- If additional changes are made after an approval, the code must be approved again.
- Be sure to merge in the `develop` branch into your feature branch to sort out any potential conflicts before getting it reviewed.
- Always delete your feature branches after merging. There is no reason for them to stick around.

## Code Review Guidelines

![](https://camo.githubusercontent.com/081a64874ee98e36a991d3619905e5c43123db5ea524ac296945c96e40fb308c/68747470733a2f2f69322e77702e636f6d2f636f6d6d61646f742e636f6d2f77702d636f6e74656e742f75706c6f6164732f323030392f30322f7774662e706e673f773d353530)

### Everyone

- Accept that many programming decisions are opinions. Discuss tradeoffs, which you prefer, and reach a resolution quickly.
- Ask good questions; don't make demands. ("What do you think about naming this `:user_id`?")
- Good questions avoid judgment and avoid assumptions about the author's perspective.
- Ask for clarification. ("I didn't understand. Can you clarify?")
- Offer clarification, explain the decisions you made to reach a solution in question.
- Be explicit. Remember people don't always understand your intentions online.
- Talk synchronously (e.g. chat, screensharing, in person) if there are too many "I didn't understand" or "Alternative solution:" comments. Pull requests should not be the place for long discussions, architectural or otherwise.
- Put notes on what's missing or could be improved in the PR description.
- Friends do not let friends have bad style. If you see someone working against the style guide, try to correct them.

### Having Your Code Reviewed

- PRs should be about one thing. If you do multiple things in one PR, it's hard to review.
- Try to keep the PRs small. There has been some research to indicate that beyond 400 LOC the ability to detect defects diminishes.
- Having a PR description is useful. Additionally, you can also link to the card on Hack 'n Plan.
- Ideally, the PR should be finished when submitted. If the PR is work in progress, add (WIP) or [WIP] to the title.
- Be aware that it can be [challenging to convey emotion and intention online](https://thoughtbot.com/blog/empathy-online).
- Explain why the code exists. ("It's like that because of these reasons. Would it be more clear if I rename this class/file/method/variable?")
- Seek to understand the reviewer's perspective.
- Try to respond to every comment. Be grateful for the reviewer's suggestions. ("Good call. I'll make that change.")
- If additional changes are made after an approval, the code must be approved again.

### Reviewing Code

- Communicate which ideas you feel strongly about and those you don't.
- Identify ways to simplify the code while still solving the problem.
- If discussions turn too philosophical or academic, move the discussion offline to a regular Friday afternoon technique discussion. In the meantime, let the author make the final decision on alternative implementations.
- Offer alternative implementations, but assume the author already considered them. ("What do you think about using a custom validator here?")
- Seek to understand the author's perspective.
- Sign off on the pull request with a 👍 or "Ready to merge" comment.
