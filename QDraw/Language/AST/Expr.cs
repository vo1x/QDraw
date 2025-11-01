public abstract class Expr
{
}

public class NumberExpr : Expr
{
    public double Value { get;  }

    public NumberExpr(double value)
    {
        Value = value;
    }
}

// variable - value pair
public class VariableExpr : Expr
{
    public Token Name { get; } 

    public VariableExpr(Token name)
    {
        Name = name;
    }
}