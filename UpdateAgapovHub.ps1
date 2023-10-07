cd $PWD
git stash --all
git sync
git stash pop
$key = $Host.UI.RawUI.ReadKey
if ($key.Character -eq '1') {
  "Pressed 1"
}