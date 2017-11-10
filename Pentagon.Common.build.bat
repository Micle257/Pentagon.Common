// change verrsion


// running cmd cmd
var proj = "Pentagon.Common";

Process cmd = new Process();
cmd.StartInfo.FileName = "cmd.exe";
cmd.StartInfo.RedirectStandardInput = true;
cmd.StartInfo.RedirectStandardOutput = true;
cmd.StartInfo.CreateNoWindow = true;
cmd.StartInfo.UseShellExecute = false;
cmd.Start();

cmd.StandardInput.WriteLine($"dotnet publish -c Release src\{proj}\");
cmd.StandardInput.WriteLine($"copy /Y build\{proj}\*.nupkg ..\..\NuGet\");
cmd.StandardInput.WriteLine($"exit");
cmd.StandardInput.Flush();
cmd.StandardInput.Close();
cmd.WaitForExit();
Console.WriteLine(cmd.StandardOutput.ReadToEnd());