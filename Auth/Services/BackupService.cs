using System.Data.SQLite;
namespace Auth.Services
{
    public interface IBackupService
    {
        void Backup();
        bool IsBackedUp();
        bool IsRestored();
        void Restore(string name);
    }

    public class BackupService : IBackupService
    {
        bool backedup, restored;
        public void Backup()
        {
            string source = @"/home/ovidiu/Documents/Projects/Final/ChatApp/Auth/Messages.db";
            string dest = @"/home/ovidiu/Databases/Messages(" + System.DateTime.Now.ToString("dd.MM.yyyy") + ").db";
            if (!System.IO.File.Exists(dest))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(dest)) { }
            }
            System.IO.File.Copy(source, dest, true);
            backedup = true;
            restored = false;
        }
        public void Restore(string name)
        {
            string source = @"/home/ovidiu/Databases/" + name;
            string dest = @"/home/ovidiu/Documents/Projects/Final/ChatApp/Auth/Messages.db";
            System.IO.File.Copy(source, dest, true);
            backedup = false;
            restored = true;
        }
        public bool IsBackedUp()
        {
            return backedup;
        }
        public bool IsRestored()
        {
            return restored;
        }
    }
}