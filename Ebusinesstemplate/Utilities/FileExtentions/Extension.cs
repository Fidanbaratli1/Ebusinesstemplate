namespace Ebusinesstemplate.Utilities.FileExtentions
{
    public static class Extension
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            if(file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool CheckFileSize(this IFormFile file,int kb)
        {
            if (file.Length < kb * 1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CheckFileAsync(this IFormFile file,string root,string folder)
        {
            string fileName=Guid.NewGuid().ToString()+file.FileName;
            string path=Path.Combine(root,folder,fileName);
            using(FileStream fileStream=new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(this string file,string root,string folder)
        {
            string path=Path.Combine(root,file,folder);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
