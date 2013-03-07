<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="ja">
<head>
    <meta charset="utf-8">
    <title>WCF ReportService Sample</title>

    <style>
        iframe {
            width: 800px;
            height: 600px;
        }
    </style>
</head>

<body>
    <div>
        <iframe src="Display.aspx?key=getKey&filename=hoge.pdf"></iframe>
    </div>
</body>
</html>
