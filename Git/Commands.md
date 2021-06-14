# Ammend commit with new date
- git commit --amend --date=now


# Combining the commits
To squash the last 3 commits into one:

Method 1

git reset --soft HEAD~3

git commit -m "New message for the combined commit"

Pushing the squashed commit
If the commits have been pushed to the remote:
git push origin +name-of-branch
The plus sign forces the remote branch to accept your rewritten history, otherwise you will end up with divergent branches

If the commits have NOT yet been pushed to the remote:
git push origin name-of-branch


Method 2

git rebase -i HEAD~3

pick last comit
use squash/fixup for all other commits

Force push to origin
