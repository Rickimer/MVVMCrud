using MVVMCrud.Data.Model;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MVVMCrud.Data.Context
{
    public class XMLContext<T> : IContext<T> where T : Entity
    {
        private string _fileName;
        public XMLContext(string fileName)
        {
            _fileName = fileName;
        }

        public ICollection<T> GetAll()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            List<T> list = new List<T>();
            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                var entity = xmlSerializer.Deserialize(fs) as T;
                list.Add(entity);
            }

            return list;
        }

        public void Add(T entity)
        {
            if (entity != null)
            {
                entity.NewId();

                XmlSerializer x = new XmlSerializer(entity.GetType());
                TextWriter writer = new StreamWriter(_fileName);
                x.Serialize(writer, entity);
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
            // десериализуем объект
            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                User? user = xmlSerializer.Deserialize(fs) as User;
                if (user != null && id.Equals(user.Id))
                {
                    var entity = user as T;
                    if (entity != null)
                    {
                        return entity;
                    }
                }
            }

            return null;
            /*var xmlDocument = new XmlDocument();
            var fileInfo = new FileInfo("person.xml");
            //xmlDocument.Load("person.xml");
            xmlDocument.Load(fileInfo.FullName);
            //var g1 = xdoc.XPathSelectElement("//word[@id='10001']");
            //var g1 = xmlDocument.XPathSelectElement("//word[@id='1c6b883a-6379-45a5-804e-7b387f131695']");
            var g1 = xmlDocument.SelectSingleNode("//*[@id='1c6b883a-6379-45a5-804e-7b387f131695']");
            var serializer = new XmlSerializer(typeof(T));
            //XElement element = new XElement();
            var xElem = XElement.Load(g1.CreateNavigator().ReadSubtree());
            //XmlReader xmlReader = new XmlReader(g1);
            var g2 = (T)serializer.Deserialize(xElem.CreateReader());
            return g2;*/
        }

        public void Update(T entity)
        {
            var xmlDocument = new XmlDocument();
            var fileInfo = new FileInfo(_fileName);
            xmlDocument.Load(fileInfo.FullName);
            var g1 = xmlDocument.SelectSingleNode($"//*[@id='{entity.Id}']");
            var xElem = XElement.Load(g1.CreateNavigator().ReadSubtree());
            xElem.Value = "";
            xmlDocument.Save(fileInfo.FullName);
            /*XDocument xdoc = XDocument.Load(_fileName);
            //var element = xdoc.Elements("MyXmlElement").Single();
            var g1 = xdoc.SelectSingleNode("//*[@id='1c6b883a-6379-45a5-804e-7b387f131695']");
            element.Value = "foo";
            xdoc.Save("file.xml");*/
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
