public abstract class Stmt{}

// STOP
public class StopStmt : Stmt
{
}

// LINE
public class LineStmt : Stmt
{
    public Expr X1 { get;  }
    public Expr Y1 { get; }
    public Expr X2 { get; }
    public Expr Y2 { get; }

    public LineStmt(Expr x1, Expr y1, Expr x2, Expr y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }
}