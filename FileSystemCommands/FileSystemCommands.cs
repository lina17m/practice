using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private readonly string _path;
    public long TotalSize { get; private set; }

    public DirectorySizeCommand(string path) => _path = path;

    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException($"Каталог '{_path}' не найден");

        var files = Directory.GetFiles(_path, "*", SearchOption.AllDirectories);
        TotalSize = files.Sum(f => new FileInfo(f).Length);
    }
}

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

    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException($"Каталог '{_path}' не найден");
        FoundFiles = Directory.GetFiles(_path, _pattern, SearchOption.AllDirectories).ToList();
    }
}
