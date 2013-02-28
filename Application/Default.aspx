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
        <iframe src="Report.aspx?key=getKey&filename=Report.pdf"></iframe>
    </div>
</body>
</html>
