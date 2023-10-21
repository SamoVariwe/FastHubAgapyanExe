cd $PWD
git stash --all
gh repo sync
git stash pop
Read-Host -Prompt "Press Enter to exit"
$key = $Host.UI.RawUI.ReadKey
if ($key.Character -eq '1') {
  "Pressed 1"
}