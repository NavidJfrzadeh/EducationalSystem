using EducationalSystem.Entities;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EducationalSystem.Services
{
    public static class DataAccess<T>
    {
        /*private readonly string _filePath = Directory.GetCurrentDirectory();

        public DataAccess(string teacherPath , string studentPath , string coursePath)
        {
            _teacherPath = teacherPath;
            _studentPath = studentPath;
            _coursePath = coursePath;
        }

        private readonly string _teacherPath;
        private readonly string _studentPath;
        private readonly string _coursePath;
        public void SaveToFile(User newUser)
        {
            var jsonData = JsonSerializer.Serialize(newUser);
            File.AppendAllText(_teacherPath, jsonData + Environment.NewLine);
        }

        public List<User> ReadFile()
        {
            var data = File.ReadAllText(_teacherPath);
            return JsonSerializer.Deserialize<List<User>>(data);
        }*/


        public static void SaveToFile(List<T> objList, string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(objList, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            File.WriteAllText(filePath, jsonData);
        }

        public static List<T> LoadFile(string filePath)
        {
            try
            {
                var data = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(data);
            }
            catch (FileNotFoundException)
            {

                File.Create(filePath).Close();
                return null;


                //if (filePath == "Students.json")
                //{
                //    DataAccess<Student>.SaveToFile(Memory.students, filePath);

                //    var studentData = File.ReadAllText(filePath);
                //    return JsonConvert.DeserializeObject<List<T>>(studentData);
                //}


                //else if (filePath == "Teachers.json")
                //{
                //    DataAccess<Teacher>.SaveToFile(Memory.teachers, filePath);

                //    var teacherData = File.ReadAllText(filePath);
                //    return JsonConvert.DeserializeObject<List<T>>(teacherData);
                //}


                //else
                //{
                //    DataAccess<Course>.SaveToFile(Memory.courses, filePath);

                //    var courseData = File.ReadAllText(filePath);
                //    return JsonConvert.DeserializeObject<List<T>>(courseData);
                //}
            }

        }
    }
}