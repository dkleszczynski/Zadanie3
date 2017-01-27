using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Task_2.Logic;
using Task_2.Models;
using System.IO;
using System.Runtime.InteropServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("FileManager")]
    [Route("")]
    public class FileManagerController : Controller
    {
        [Route("DisplayFileMetadata/{filename}")]
        public IActionResult DisplayFileMetadata(string fileName)
        {
            string filePath = "";
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        filePath = drive.Name + fileName;
                        break;
                    }
                }
            }
            else
                filePath = "/home/" + fileName;
            
            FileManager fm = new FileManager();
            FileData file = fm.GetFile(filePath);

            return View(file);
        }

        [Route("DisplayFilesMetadata")]
        [Route("")]
        public IActionResult DisplayFilesMetadata()
        {
            string directoryPath = "";
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        directoryPath = drive.Name;
                        break;
                    }
                }
            }
            else
                directoryPath = "/home";


            FileManager fm = new FileManager();
            List<FileData> files = fm.GetFiles(directoryPath);

            return View(files);
        }

        [Route("GetFileMetadata/{fileName}")]
        public IActionResult GetFileMetadata(string fileName)
        {
            string filePath = "";
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        filePath = drive.Name + fileName;
                        break;
                    }
                }
            }
            else
                filePath = "/home/" + fileName;


            FileManager fm = new FileManager();
            FileData file = fm.GetFile(filePath);
            FileDataSimple simpleFile = new FileDataSimple(file);

            return Json(simpleFile);
        }

        [Route("GetFilesMetadata")]
        public IActionResult GetFilesMetadata()
        {
            string directoryPath = "";
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        directoryPath = drive.Name;
                        break;
                    }
                }
            }
            else
                directoryPath = "/home";


            FileManager fm = new FileManager();
            List<FileData> files = fm.GetFiles(directoryPath);
            List<FileDataSimple> simpleFiles = new List<FileDataSimple>();

            foreach (FileData fd in files)
                simpleFiles.Add(new FileDataSimple(fd));

            return Json(simpleFiles);
        }
    }
}
