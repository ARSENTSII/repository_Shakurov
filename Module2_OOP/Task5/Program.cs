using System;

class Program
{

    static void Main()
    {
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat();

        sensor.TemperatureChanged += thermostat.OnTemperatureChanged;

        sensor.SetTemparature(15);
        sensor.SetTemparature(22);
        sensor.SetTemparature(10);
    }
}

delegate void TemperatureChanged(double newTemperature);

class TemperatureSensor
{
    public event TemperatureChanged TemperatureChanged;

    private double temperature;

    public void SetTemparature(double newTemperature)
    {
        temperature = newTemperature;

        if (TemperatureChanged != null)
        {
            TemperatureChanged(temperature);
        }
    }
}

class Thermostat
{
    private bool heatingOn = false;

    public void OnTemperatureChanged(double newTemperature)
    {
        Console.WriteLine($"Термостат получил новую температуру: {newTemperature}");

        if (newTemperature < 18)
        {
            heatingOn = true;
            Console.WriteLine("Отопление включено");
        }

        else
        {
            heatingOn = false;
            Console.WriteLine("Отопление выключено");
        }
    }
}

