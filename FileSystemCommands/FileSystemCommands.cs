using CommandLib; 
namespace FileSystemCommands;

[DisplayName("Команда расчета размера директории")]
[Version(1, 0)]
public class DirectorySizeCommand : ICommand
{
    private readonly string _path;
    public long TotalSize { get; private set; }
    public DirectorySizeCommand(string path) => _path = path;
    [DisplayName("Вычислить суммарный размер всех файлов")]
    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException($"Каталог '{_path}' не найден");
        var files = Directory.GetFiles(_path, "*", SearchOption.AllDirectories);
        TotalSize = files.Sum(f => new FileInfo(f).Length);
        
        Console.WriteLine($"Размер каталога: {TotalSize} байт");
    }
}

[DisplayName("Команда поиска файлов по маске")]
[Version(1, 1)]
public class FindFilesCommand : ICommand
{
    private readonly string _path;
    private readonly string _pattern;
    public List<string> FoundFiles { get; private set; } = new();
    public FindFilesCommand(string path, string pattern)
    {
        _path = path;
        _pattern = pattern;
    }

    [DisplayName("Найти файлы в каталоге")]
    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException($"Каталог '{_path}' не найден");
        FoundFiles = Directory.GetFiles(_path, _pattern, SearchOption.AllDirectories).ToList();
        
        Console.WriteLine($"Найдено файлов: {FoundFiles.Count}");
    }
}
