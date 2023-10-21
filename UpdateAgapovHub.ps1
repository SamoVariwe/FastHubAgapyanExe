git fetch --all
git reset --hard origin/master
Read-Host -Prompt "Press Enter to exit"
$key = $Host.UI.RawUI.ReadKey
if ($key.Character -eq '1') {
  "Pressed 1"
}