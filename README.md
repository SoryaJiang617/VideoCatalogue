# VideoCatalogue (ASP.NET Core MVC + xUnit Tests)

This is a small Video Catalogue web application where users can upload mp4 files
and then view them in the browser with a built–in player UI.  
I built this project based on the challenge requirement to demonstrate my .NET,
MVC pattern understanding, file handling and basic testing ability.

---

## Tech Stack

| Area | Technology |
|---|---|
| Backend | ASP.NET Core Web MVC (.NET 8) |
| Views | Razor Pages |
| File Storage | Local `/media` folder mapped via `StaticFileOptions` |
| Testing | xUnit + Integration Tests |
| Styling | Bootstrap basic layout |

---

## Features Implemented

- Upload multiple `.mp4` files at once
- Store files locally under `/media`
- List available videos in a table with file size
- Click row → load and play video immediately
- Simple search box (filter by file name)
- Pagination (improves UX when many files)
- Delete single video or delete all
- xUnit tests included

I tried to keep the UI simple and clean, and focus more on correct behaviour and structure.

---

## Project Structure

VideoCatalogue.sln
│
├── VideoCatalogue/ # Main ASP.NET Core MVC project
│ ├── Controllers/
│ ├── Views/
│ ├── wwwroot/media/ # Uploaded video storage
│ └── appsettings.json
│
└── VideoCatalogue.Tests/ # xUnit test project

---

```bash
git clone https://github.com/SoryaJiang617/VideoCatalogue.git
cd VideoCatalogue
dotnet run --project VideoCatalogue
Open the browser:

http://localhost:7215

You can start uploading and playing videos immediately.

Run Automated Tests
cd VideoCatalogue.Tests
dotnet test
