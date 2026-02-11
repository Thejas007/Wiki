# 1) Can you SSH to GitHub?
ssh -T git@github.com
# You should see something like: "Hi <username>! You've successfully authenticated..."

# 2) If that fails, generate a key (if you don't already have one)
ssh-keygen -t ed25519 -C "username@example.com"

# 3) Start ssh-agent and add your key
eval "$(ssh-agent -s)"
ssh-add ~/.ssh/id_ed25519

# 4) Copy the public key and add it in GitHub → Settings → SSH and GPG keys
cat ~/.ssh/id_ed25519.pub
