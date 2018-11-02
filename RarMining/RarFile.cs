using System;
using System.Threading.Tasks;
using RarMining;

class RarFile
{

    public Unrar UnrarObj { get; set; }
    public bool IsCompleted { get; set; }
    private GeneratePassword PasswdList { get; set; }
    public bool IsGeneratePasswd { get; set; }
    public RarFile(string dirOfFile, string nameOfFile,string Passwd)
    {
      
        UnrarObj = new Unrar();

        // Open archive for extraction
        UnrarObj.DestinationPath = dirOfFile;
        UnrarObj.Open(dirOfFile + nameOfFile, Unrar.OpenMode.Extract);
        UnrarObj.Password = Passwd;
    }
    public RarFile(string dirOfFile, string nameOfFile, bool isGeneratePasswd,int lengthOfPasswd,string setOfChar)
    {
        IsCompleted = false;
        UnrarObj = new Unrar();

        IsGeneratePasswd = isGeneratePasswd;
        // Open archive for extraction
        UnrarObj.DestinationPath = @"E:\";
        UnrarObj.Open(dirOfFile + nameOfFile, Unrar.OpenMode.Extract);
        PasswdList=new GeneratePassword(lengthOfPasswd, setOfChar);
    }

    public void CheckPass()
    {
        Unrar tmpRar = UnrarObj;
        //Unrar tmpRar = new Unrar(UnrarObj.ArchivePathName);
        //tmpRar.DestinationPath = @"E:\";
        //tmpRar.Open(UnrarObj.DestinationPath, Unrar.OpenMode.Extract);
        
        while (!IsCompleted || tmpRar.Password !="DONE")
        try
        {

            tmpRar.Password = PasswdList.GetNextPasswd();
            while (UnrarObj.ReadHeader())
            {
                tmpRar.Extract();
            }
            System.Diagnostics.Debug.WriteLine("Pass found:" + tmpRar.Password);
            IsCompleted = true;
        }
        catch
        {
            System.Diagnostics.Debug.WriteLine("Wrong pass:"+ tmpRar.Password);
            if (tmpRar != null)
                tmpRar.Close();
            continue;
        }
    }
    public void MulithreadExtract(int numOfThread)
    {
        //for (int i = 0; i < numOfThread; i++)
        //{
        //    Task task=new Task(CheckPass);
        //    task.Start();
        //}
        CheckPass();

        
    }
    public void DoExtract()
    {
        try
        {
            while (UnrarObj.ReadHeader())
            {
                UnrarObj.Extract();
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (this.UnrarObj != null)
                UnrarObj.Close();
        }
    }

}