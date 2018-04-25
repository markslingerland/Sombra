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
  docker volume create --name=sqlserverdata
  docker volume create --name=mongoserverdata
  docker volume create --name=rabbitmqdata
  docker volume create --name=portainerdata
  docker-compose build
  DC_RABBITMQPASSWORD=$DC_RABBITMQPASSWORD DC_SQLPASSWORD=$DC_SQLPASSWORD DC_MONGOPASSWORD=$DC_MONGOPASSWORD DC_SMTPSERVER=$DC_SMTPSERVER DC_SMTPPORT=$DC_SMTPPORT DC_SMTPUSERNAME=$DC_SMTPUSERNAME DC_SMTPPASSWORD=$DC_SMTPPASSWORD docker-compose up -d
  docker ps
  docker system prune -a -f
EOF