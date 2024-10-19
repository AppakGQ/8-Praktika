using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void On() => Console.WriteLine("Свет включен.");
    public void Off() => Console.WriteLine("Свет выключен.");
}

public class AirConditioner
{
    public void On() => Console.WriteLine("Кондиционер включен.");
    public void Off() => Console.WriteLine("Кондиционер выключен.");
    public void SetTemperature(int temperature) => Console.WriteLine($"Температура установлена на {temperature} градусов.");
}

public class Television
{
    public void On() => Console.WriteLine("Телевизор включен.");
    public void Off() => Console.WriteLine("Телевизор выключен.");
    public void SetChannel(int channel) => Console.WriteLine($"Канал переключен на {channel}.");
}

public class LightOnCommand : ICommand
{
    private Light _light;
    public LightOnCommand(Light light) => _light = light;
    public void Execute() => _light.On();
    public void Undo() => _light.Off();
}

public class LightOffCommand : ICommand
{
    private Light _light;
    public LightOffCommand(Light light) => _light = light;
    public void Execute() => _light.Off();
    public void Undo() => _light.On();
}

public class AirConditionerOnCommand : ICommand
{
    private AirConditioner _ac;
    public AirConditionerOnCommand(AirConditioner ac) => _ac = ac;
    public void Execute() => _ac.On();
    public void Undo() => _ac.Off();
}

public class AirConditionerOffCommand : ICommand
{
    private AirConditioner _ac;
    public AirConditionerOffCommand(AirConditioner ac) => _ac = ac;
    public void Execute() => _ac.Off();
    public void Undo() => _ac.On();
}

public class TelevisionOnCommand : ICommand
{
    private Television _tv;
    public TelevisionOnCommand(Television tv) => _tv = tv;
    public void Execute() => _tv.On();
    public void Undo() => _tv.Off();
}

public class TelevisionOffCommand : ICommand
{
    private Television _tv;
    public TelevisionOffCommand(Television tv) => _tv = tv;
    public void Execute() => _tv.Off();
    public void Undo() => _tv.On();
}

public class MacroCommand : ICommand
{
    private List<ICommand> _commands;
    public MacroCommand(List<ICommand> commands) => _commands = commands;
    public void Execute()
    {
        foreach (var command in _commands)
            command.Execute();
    }
    public void Undo()
    {
        foreach (var command in _commands)
            command.Undo();
    }
}

public class RemoteControl
{
    private ICommand[] _onCommands;
    private ICommand[] _offCommands;
    private Stack<ICommand> _commandHistory;

    public RemoteControl()
    {
        _onCommands = new ICommand[3];
        _offCommands = new ICommand[3];
        _commandHistory = new Stack<ICommand>();
    }

    public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
    {
        _onCommands[slot] = onCommand;
        _offCommands[slot] = offCommand;
    }

    public void OnButtonWasPushed(int slot)
    {
        _onCommands[slot].Execute();
        _commandHistory.Push(_onCommands[slot]);
    }

    public void OffButtonWasPushed(int slot)
    {
        _offCommands[slot].Execute();
        _commandHistory.Push(_offCommands[slot]);
    }

    public void UndoButtonWasPushed()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand command = _commandHistory.Pop();
            command.Undo();
        }
    }
}

class Program
{
    static void Main()
    {
        RemoteControl remote = new RemoteControl();

        Light livingRoomLight = new Light();
        AirConditioner ac = new AirConditioner();
        Television tv = new Television();

        LightOnCommand lightOn = new LightOnCommand(livingRoomLight);
        LightOffCommand lightOff = new LightOffCommand(livingRoomLight);
        AirConditionerOnCommand acOn = new AirConditionerOnCommand(ac);
        AirConditionerOffCommand acOff = new AirConditionerOffCommand(ac);
        TelevisionOnCommand tvOn = new TelevisionOnCommand(tv);
        TelevisionOffCommand tvOff = new TelevisionOffCommand(tv);

        remote.SetCommand(0, lightOn, lightOff);
        remote.SetCommand(1, acOn, acOff);
        remote.SetCommand(2, tvOn, tvOff);

        remote.OnButtonWasPushed(0);
        remote.OffButtonWasPushed(0);
        remote.UndoButtonWasPushed();

        remote.OnButtonWasPushed(1);
        remote.OffButtonWasPushed(1);
        remote.UndoButtonWasPushed();

        List<ICommand> partyCommands = new List<ICommand> { lightOn, acOn, tvOn };
        MacroCommand partyMacro = new MacroCommand(partyCommands);
        partyMacro.Execute();
        partyMacro.Undo();
    }
}
