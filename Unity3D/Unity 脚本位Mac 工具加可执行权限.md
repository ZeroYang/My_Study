# Unity 脚本位Mac 工具加可执行权限

        string procssPath = Application.dataPath.Replace("/Assets", "") + "/pdb2mdb/ztoolkit_osx";
        using (System.Diagnostics.Process cmd = new System.Diagnostics.Process()) {
            cmd.StartInfo.FileName = "chmod";
            cmd.StartInfo.Arguments = string.Concat("+x ", procssPath);
            cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.WaitForExit();
        }