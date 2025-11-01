using System.Collections.Generic;

public class Lexer
{
    private readonly string _source;
    private readonly List<Token> _tokens = new List<Token>();
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    private static readonly Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
    {
        { "LINE", TokenType.LINE },
        { "STOP", TokenType.STOP }
    };

    public Lexer(string source)
    {
        _source = source;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }
        
        _tokens.Add(new Token(TokenType.EOF, "", null,_line));
        return _tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(TokenType.LPAREN); break;
            case ')': AddToken(TokenType.RPAREN); break;
            case ',': AddToken(TokenType.COMMA); break;
            
            
            case ' ': 
            case '\r':
            case '\t':
                break;
            
            case '\n':
                _line++;
                break;
            
            default:
                if (IsDigit(c))
                {
                    HandleNumber();
                }else if (IsAlpha(c))
                {
                    HandleIdentifier();
                }
                else
                {
                    Console.WriteLine($"[Line {_line}] Error: Unexpected character '{c}'.");
                }
                break;
        }
    }
    
    
    // type handlers
    private void HandleIdentifier()
    {
        while (IsAlphaNumeric(Peek())) Advance();

        string text = _source.Substring(_start, _current - _start);
        
        if(_keywords.TryGetValue(text.ToUpper(), out TokenType type))
        {
            AddToken(type);
        }
        else
        {
            AddToken(TokenType.IDENTIFIER);
        }
    }

    private void HandleNumber()
    {
        while (IsDigit(Peek())) Advance();
        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Advance();
            while (IsDigit(Peek())) Advance();
        }

        double value = double.Parse(_source.Substring(_start, _current - _start));
        AddToken(TokenType.NUMBER, value);
        
        
    }
    
    // helpers

    private char Advance() => _source[_current++];
    private bool IsAtEnd() => _current >= _source.Length;
    private char Peek() => IsAtEnd() ? '\0' : _source[_current];
    private char PeekNext() => (_current + 1 >= _source.Length) ? '\0' : _source[_current + 1];


    private bool IsDigit(char c) => c >= '0' && c <= '9';
    private bool IsAlpha(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
    private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsDigit(c);
    
    
    private void AddToken(TokenType type, object literal = null)
    {
        string text = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, text, literal, _line));
    }
}