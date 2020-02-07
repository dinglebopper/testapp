using System;
using System.IO;
using System.Reflection;
using WebSocketSharp.Server;
using WebSocketSharp.Net;
using System.Text;
using System.Collections.Generic;
using WebSocketSharp;
using System.Text.RegularExpressions;

namespace TestApp
{
    public class Server
    {
        private static readonly Dictionary<string, string> kMimeTypes_ =
            new Dictionary<string, string>()
        {
            { "gif", "image/gif" },
            { "js", "application/javascript" },
            { "html", "text/html" },
            { "css", "text/css" },
            { "jpg", "image/jpeg" }
        };

        private const int kDefaultPort_ = 8080;

        private static readonly System.Net.IPAddress kDefaultHost_ = System.Net.IPAddress.Any;

        private WebSocketSharp.Server.HttpServer server_ = null;

        private static readonly string kDefaultStaticPath_ = Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAss‌​embly().Location),
            "static");

        public void Start()
        {
            server_ = new WebSocketSharp.Server.HttpServer(kDefaultHost_, kDefaultPort_);

            server_.OnGet += handleHttpRequest_;
            server_.OnPost += handleHttpRequest_;
            server_.OnPut += handleHttpRequest_;
            server_.OnPatch += handleHttpRequest_;
            server_.OnTrace += handleHttpRequest_;
            server_.OnDelete += handleHttpRequest_;
            server_.OnOptions += handleHttpRequest_;

            server_.Start();
        }

        public void Stop()
        {
            server_?.Stop();
        }

        public static string GetMimeType(string extension, string fallback = "text/plain")
        {
            string ext = extension.ToLower();

            if (kMimeTypes_.ContainsKey(ext))
            {
                return kMimeTypes_[ext];
            }

            return fallback;
        }

        private void handleHttpRequest_(object sender, HttpRequestEventArgs e)
        {
            HttpListenerRequest req = e.Request;
            HttpListenerResponse res = e.Response;
            var method = e.Request.HttpMethod.ToUpper();
            var path = req.Url.AbsolutePath;

            try
            {
                if (method == "GET" && path == "/") handleLanding_(req, res);
                else if (method == "GET" && path == "/send") handleSend_(req, res);
                else handleStatic_(req, res);
            }
            catch(Exception ex)
            {
                res.ContentType = "text/plain";
                res.StatusCode = 500;
                writeContent_(res, ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private void sendFile_(HttpListenerRequest req, HttpListenerResponse res,
            string path, string mime = "plain/text")
        {
            writeFile_(res, path, mime);
        }

        private void writeContent_(HttpListenerResponse res, byte[] content)
        {
            res.WriteContent(content);
        }

        private void writeContent_(HttpListenerResponse res, string content)
        {
            res.WriteContent(Encoding.UTF8.GetBytes(content));
        }

        private void writeFile_(HttpListenerResponse res, string path, string mime)
        {
            // If the file is not found, return a 404
            if (!File.Exists(path))
            {
                res.ContentType = mime;
                res.StatusCode = 404;
                writeContent_(res, "File not found :o");
                return;
            }

            res.ContentType = mime;
            res.StatusCode = 200;
            writeContent_(res, File.ReadAllBytes(path));
        }

        private void handleLanding_(HttpListenerRequest req, HttpListenerResponse res)
        {
            writeFile_(res, Path.Combine(kDefaultStaticPath_, "html", "index.html"), "text/html");
        }

        private void handleStatic_(HttpListenerRequest req, HttpListenerResponse res)
        {
            string file = Path.Combine(kDefaultStaticPath_, Regex.Replace(
                req.Url.AbsolutePath.Replace("/", @"\"), @"^[\\]+", ""));

            // If the file is not found, return a 404
            if (!File.Exists(file))
            {
                res.ContentType = "text/plain";
                res.StatusCode = 404;
                writeContent_(res, "File not found :o");
                return;
            }

            // The file exists, deliver it
            res.ContentType = Server.GetMimeType(Path.GetExtension(
                Path.GetFileName(req.Url.AbsolutePath)).Substring(1),
                "text/html");
            res.StatusCode = 200;
            res.WriteContent(File.ReadAllBytes(file));
        }

        private void handleSend_(HttpListenerRequest req, HttpListenerResponse res)
        {
            SenderType type = (SenderType)Convert.ToInt32(req.QueryString["type"]);
            string from = req.QueryString["from"];
            string to = req.QueryString["to"];
            string msg = req.QueryString["message"];

            ISender sender = SenderFactory.Get(type);
            res.ContentType = "application/json";
            res.StatusCode = 200;
            writeContent_(res, "{\"response\":\"" + sender.Send(from, to, msg) + "\"}");
        }
    }
}
