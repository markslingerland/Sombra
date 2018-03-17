#!/bin/bash

eval "$(ssh-agent -s)" # Start ssh-agent cache
chmod 600 rsa # Allow read access to the private key
ssh-add rsa # Add the private key to SSH

git config --global push.default matching
git remote add deploy ssh://git@$IP:$PORT$DEPLOY_DIR
git push deploy master

# Skip this command if you don't need to execute any additional commands after deploying.
ssh docker-deployment@$IP -p $PORT <<EOF
  yes
  cd $DEPLOY_DIR
  docker-compose up
EOF