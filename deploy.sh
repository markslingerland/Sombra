#!/bin/bash
set -x # Show the output of the following commands (useful for debugging)
chmod 600 rsa
mv rsa ~/.ssh/rsa
eval "$(ssh-agent -s)" # Start ssh-agent cache
ssh-add ~/.ssh/rsa
chmod 600 config.txt
mv config.txt ~/.ssh/config
ls
git config --global push.default matching
git remote add deploy "$USER@$IP:$DEPLOY_DIR"
git remote add old https://github.com/markslingerland/Sombra.git
git fetch --unshallow old
git push --force deploy master -v

# Skip this command if you don't need to execute any additional commands after deploying.
ssh -o "StrictHostKeyChecking no" $USER@$IP -p $PORT <<EOF
  cd $DEPLOY_DIR
  cd ..
  cd $DIR
  git pull 
  docker-compose build
  docker-compose up -d
  docker ps
EOF