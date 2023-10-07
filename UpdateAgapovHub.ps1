cd $PWD
git stash --all
gh repo sync
git stash pop
$key = $Host.UI.RawUI.ReadKey
if ($key.Character -eq '1') {
  "Pressed 1"
}