using System;

namespace Model8_Practic_work
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class Light
    {
        public void On()
        {
            Console.WriteLine("Свет включен.");
        }

        public void Off()
        {
            Console.WriteLine("Свет выключен.");
        }
    }
    public class AirConditioner
    {
        public void On()
        {
            Console.WriteLine("Кондиционер включен.");
        }

        public void Off()
        {
            Console.WriteLine("Кондиционер выключен.");
        }
    }
    public class Television
    {
        public void On()
        {
            Console.WriteLine("Телевизор включен.");
        }

        public void Off()
        {
            Console.WriteLine("Телевизор выключен.");
        }
    }
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.On();
        }

        public void Undo()
        {
            _light.Off();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.Off();
        }

        public void Undo()
        {
            _light.On();
        }
    }
    public class AirConditionerOnCommand : ICommand
    {
        private AirConditioner _airConditioner;

        public AirConditionerOnCommand(AirConditioner airConditioner)
        {
            _airConditioner = airConditioner;
        }

        public void Execute()
        {
            _airConditioner.On();
        }

        public void Undo()
        {
            _airConditioner.Off();
        }
    }

    public class AirConditionerOffCommand : ICommand
    {
        private AirConditioner _airConditioner;

        public AirConditionerOffCommand(AirConditioner airConditioner)
        {
            _airConditioner = airConditioner;
        }

        public void Execute()
        {
            _airConditioner.Off();
        }

        public void Undo()
        {
            _airConditioner.On();
        }
    }
    public class TelevisionOnCommand : ICommand
    {
        private Television _television;

        public TelevisionOnCommand(Television television)
        {
            _television = television;
        }

        public void Execute()
        {
            _television.On();
        }

        public void Undo()
        {
            _television.Off();
        }
    }

    public class TelevisionOffCommand : ICommand
    {
        private Television _television;

        public TelevisionOffCommand(Television television)
        {
            _television = television;
        }

        public void Execute()
        {
            _television.Off();
        }

        public void Undo()
        {
            _television.On();
        }
    }
    public class RemoteControl
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _lastCommand;

        public RemoteControl()
        {
            _onCommands = new ICommand[5];  // 5 слотов для команд
            _offCommands = new ICommand[5];
            _lastCommand = null;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonWasPressed(int slot)
        {
            if (_onCommands[slot] != null)
            {
                _onCommands[slot].Execute();
                _lastCommand = _onCommands[slot];
            }
            else
            {
                Console.WriteLine("Команда не назначена.");
            }
        }

        public void OffButtonWasPressed(int slot)
        {
            if (_offCommands[slot] != null)
            {
                _offCommands[slot].Execute();
                _lastCommand = _offCommands[slot];
            }
            else
            {
                Console.WriteLine("Команда не назначена.");
            }
        }

        public void UndoButtonWasPressed()
        {
            if (_lastCommand != null)
            {
                _lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("Отмена невозможна.");
            }
        }
    }
    public class MacroCommand : ICommand
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            foreach (ICommand command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            foreach (ICommand command in _commands)
            {
                command.Undo();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем устройства
            Light livingRoomLight = new Light();
            AirConditioner airConditioner = new AirConditioner();
            Television television = new Television();

            // Создаем команды для управления устройствами
            LightOnCommand lightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand lightOff = new LightOffCommand(livingRoomLight);
            AirConditionerOnCommand acOn = new AirConditionerOnCommand(airConditioner);
            AirConditionerOffCommand acOff = new AirConditionerOffCommand(airConditioner);
            TelevisionOnCommand tvOn = new TelevisionOnCommand(television);
            TelevisionOffCommand tvOff = new TelevisionOffCommand(television);

            // Создаем пульт
            RemoteControl remoteControl = new RemoteControl();
            remoteControl.SetCommand(0, lightOn, lightOff);
            remoteControl.SetCommand(1, acOn, acOff);
            remoteControl.SetCommand(2, tvOn, tvOff);

            // Тестируем команды
            remoteControl.OnButtonWasPressed(0);  // Включаем свет
            remoteControl.OffButtonWasPressed(0); // Выключаем свет
            remoteControl.UndoButtonWasPressed(); // Отменяем выключение света

            remoteControl.OnButtonWasPressed(1);  // Включаем кондиционер
            remoteControl.OffButtonWasPressed(1); // Выключаем кондиционер
            remoteControl.UndoButtonWasPressed(); // Отменяем выключение кондиционера

            // Макрокоманда
            ICommand[] partyMode = { lightOn, acOn, tvOn };
            MacroCommand partyMacro = new MacroCommand(partyMode);

            // Выполнение макрокоманды
            Console.WriteLine("Выполняем макрокоманду 'Party Mode'");
            partyMacro.Execute();
            Console.WriteLine("Отмена макрокоманды 'Party Mode'");
            partyMacro.Undo();

            Console.ReadKey();
        }
    }
}
