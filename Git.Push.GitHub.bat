git checkout master
git checkout -B mastertogit
git filter-branch -f --env-filter "GIT_AUTHOR_NAME='Micle257'; GIT_AUTHOR_EMAIL='imiclelol@gmail.com'; GIT_COMMITTER_NAME='Micle257'; GIT_COMMITTER_EMAIL='imiclelol@gmail.com';" HEAD
git push -u github

git checkout dev
git checkout -B devtogit
git filter-branch -f --env-filter "GIT_AUTHOR_NAME='Micle257'; GIT_AUTHOR_EMAIL='imiclelol@gmail.com'; GIT_COMMITTER_NAME='Micle257'; GIT_COMMITTER_EMAIL='imiclelol@gmail.com';" HEAD
git push -u github

git branch -D mastertogit && git branch -D devtogit