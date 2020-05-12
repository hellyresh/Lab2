
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics;


namespace Lab2_Sokolova_Transport
{
    /// <summary>
    /// Класс, обозначающий абстрактный транспорт
    /// </summary>
    public abstract class My_transport
    {
        /// <summary>
        /// Функция, выводящая данные
        /// </summary>
        abstract public void Info();

        /// <summary>
        /// Функция, вычисляющая грузоподъемность
        /// </summary>
        abstract public double Carrying();

        
    }

    /// <summary>
    /// Пассажирская машина
    /// </summary>
    public class PassengerCar : My_transport
    {
        /// <summary>
        /// Марка
        /// </summary>
        public string label;
        /// <summary>
        /// Номер
        /// </summary>
        public string number;
        /// <summary>
        /// Скорость
        /// </summary>
        public double speed;
        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public double carrying;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="label">Марка</param>
        /// <param name="number">Номер</param>
        /// <param name="speed">Скорость</param>
        /// <param name="carrying">Грузоподъемность</param>
        public PassengerCar(string label, string number, double speed, double carrying)
        {
            this.label = label;
            this.number = number;
            this.speed = speed;
            this.carrying = carrying;
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public PassengerCar() : base()
        {

        }
        public override void Info()
        {
            Trace.WriteLine("Вызвана функция Info");
            Console.WriteLine("Марка автомобиля: {0} \nСкорость: {1} \nНомер: {2} \nГрузоподьемность: {3} \n", label, speed, number, carrying);
        }
        public override double Carrying()
        {
            Trace.WriteLine("Вызвана функция Carrying");
            return this.carrying;
        }
    }

    /// <summary>
    /// Мотоцикл
    /// </summary>
    public class Motorcycle : PassengerCar
    {
        /// <summary>
        /// Наличие коляски
        /// </summary>
        public bool sidecar;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="label">Марка</param>
        /// <param name="number">Номер</param>
        /// <param name="speed">Скорость</param>
        /// <param name="carrying">Грузоподъемность</param>
        /// <param name="sidecar">Наличие коляски</param>
        public Motorcycle(string label, string number, double speed, double carrying, bool sidecar) : base(label, number, speed, carrying)
        {
            this.sidecar = sidecar;
            if (sidecar == false)
                this.carrying = 0;
            else
                this.carrying = carrying;
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Motorcycle() 
        {
        }
        public override void Info()
        {
            Trace.WriteLine("Вызвана функция Info");
            string rusSidecar;
            if (sidecar) rusSidecar = "да";
            else rusSidecar = "нет";

            Console.WriteLine("Марка мотоцикла: {0} \nСкорость: {1} \nНомер: {2} \nГрузоподьемность: {3}\nНаличие коляски: {4} \n", label, speed, number, carrying, rusSidecar);

        }
        public override double Carrying()
        {
            Trace.WriteLine("Вызвана функция Carrying");
            return this.carrying;
        }

    }

    /// <summary>
    /// Грузовик
    /// </summary>
    public class Truck : PassengerCar
    {
        /// <summary>
        /// Наличие прицепа
        /// </summary>
        public bool trailer;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="label">Марка</param>
        /// <param name="number">Номер</param>
        /// <param name="speed">Скорость</param>
        /// <param name="carrying">Грузоподъемность</param>
        /// <param name="trailer">Наличие прицепа</param>
        public Truck(string label, string number, double speed, double carrying, bool trailer) : base(label, number, speed, carrying)
        {
            this.trailer = trailer;

            if (trailer == true)
                this.carrying = 2 * this.carrying;
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Truck() 
        { }
        public override void Info()
        {
            Trace.WriteLine("Вызвана функция Info");
            string rusTrailer;
            if (trailer) rusTrailer = "да";
            else rusTrailer = "нет";
            Console.WriteLine("Марка грузового автомобиля: {0} \nСкорость: {1} \nНомер: {2} \nГрузоподьемность: {3}\nНаличие Прицепа: {4} \n\n", label, speed, number, carrying, rusTrailer);

        }
        public override double Carrying()
        {
            Trace.WriteLine("Вызвана функция Carrying");
            return this.carrying;
        }
    }

    /// <summary>
    /// Класс, осуществляющий сериализацию любой фигуры
    /// </summary>
    public class TransportXMLSerializer
    {
        /// <summary>
        /// Сериализатор пассажирских машин
        /// </summary>
        private XmlSerializer passSerializer;

        /// <summary>
        /// Сериализатор мотоциклов
        /// </summary>
        private XmlSerializer motoSerializer;

        /// <summary>
        /// Сериализатор грузовиков
        /// </summary>
        private XmlSerializer truckSerializer;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TransportXMLSerializer()
        {
            passSerializer = new XmlSerializer(typeof(PassengerCar));
            motoSerializer = new XmlSerializer(typeof(Motorcycle));
            truckSerializer = new XmlSerializer(typeof(Truck));
        }

        public void Serialize(Stream stream, Object o)
        {
            Trace.WriteLine("Вызвана функция Serialize");
            switch (o.GetType().Name)
            {
                case "PassengerCar":
                    passSerializer.Serialize(stream, o);
                    break;
                case "Motorcycle":
                    motoSerializer.Serialize(stream, o);
                    break;
                case "Truck":
                    truckSerializer.Serialize(stream, o);
                    break;                
            }
        }
    }
    class Program
    {
        /// <summary>
        /// Функция, выполняющая приведение строки к числу формата Double
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Полученное число формата Double</returns>
        public static double TryParseDouble(string input)
        {
            Trace.WriteLine("Вызвана функция TryParseDouble");

            double answer = 0;
            if (!Double.TryParse(input, out answer))
                throw new FormatException();

            return answer;
        }
        /// <summary>
        /// Функция, выполняющая приведение строки к числу формата Int
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Полученное число формата Int</returns>
        public static int TryParseInt(string input)
        {
            Trace.WriteLine("Вызвана функция TryParseInt");

            int answer = 0;
            if (!Int32.TryParse(input, out answer))
                throw new FormatException();
            return answer;
        }
        /// <summary>
        /// Функция, выполняющая приведение типа Boolean в слово
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Полученное число формата Int</returns>
        public static string ParseBool(bool input)
        {
            Trace.WriteLine("Вызвана функция ParseBool");
            switch (input)
            {
                case true:
                    return "да";
                default: return "нет";                    
            }
        }
        /// <summary>
        /// Функция, выполняющая приведение строки к числу формата Bool
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Полученное число формата Bool</returns>
        public static bool TryParseBool(string input)
        {
            Trace.WriteLine("Вызвана функция TryParseBool");

            bool answer = false;
            if (!Boolean.TryParse(input, out answer))
                throw new FormatException();
            return answer;
        }
        /// <summary>
        /// Функция, определяющая транспорт как объект
        /// </summary>
        /// <param name="line">Входная строка</param>
        /// <returns>Объект типа транспорт</returns>
        public static My_transport ParseTransport(string line)
        {
            Trace.WriteLine("Вызвана функция ParseTransport");
            var args = line.Split(' ');
            switch (args[0])
            {
                case "PassengerCar":
                    return new PassengerCar(args[1], args[2], TryParseDouble(args[3]), TryParseDouble(args[4]));
                case "Motorcycle":
                    return new Motorcycle(args[1], args[2], TryParseDouble(args[3]), TryParseDouble(args[4]), TryParseBool(args[5]));
                case "Truck":
                    return new Truck(args[1], args[2], TryParseDouble(args[3]), TryParseDouble(args[4]), TryParseBool(args[5]));
                default:
                    throw new FormatException();
            }
        }
        /// <summary>
        /// Функция поиска машины, удовлетворяющей заданной грузоподъемности
        /// </summary>
        /// <param name="Ob">Массив машин</param>
        /// <param name="carrying">Грузоподъемность</param>
        public static void SearchCarrying(My_transport[] Ob, double carrying)
        {
            foreach (My_transport i in Ob)
            {
                double p = i.Carrying();
                if (carrying <= p)
                {
                    i.Info();
                }
            }
        }
        static void Main(string[] args)
        {
            int n;

            try
            {
                using (var inputStream = new FileStream("input.txt", FileMode.Open))
                using (var streamReader = new StreamReader(inputStream))
                {
                    var logStream = new FileStream("log.txt", FileMode.Create);
                    var figureXMLSerializer = new TransportXMLSerializer();
                    n = TryParseInt(streamReader.ReadLine());
                    My_transport[] transportArray = new My_transport[n];
                    int cnt = 0;
                    for (int i = 0; i < n; i++)
                    {
                        string line = streamReader.ReadLine();
                        try
                        {
                            var transport = ParseTransport(line);
                            transportArray[cnt] = transport;
                            cnt++;
                            transport.Info();
                            figureXMLSerializer.Serialize(logStream, transport);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(String.Format("Строка {0}: {1}", (i + 1).ToString(), e.Message));
                        }
                    }
                    Console.WriteLine("Поиск машин с подходящей грузоподъемностью: \n");
                    SearchCarrying(transportArray, 100);
                    
                    logStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadLine();
        }
    }
}
