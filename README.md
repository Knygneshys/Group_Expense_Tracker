Pagal nutylėjimą Vite React veikia ant PORT: 5173. Tad tokį port ir pateikiau back-end'ui appsettings.json faile:
"frontend_url": "http://localhost:5173"

.NET Core Swagger gali pasileisti ant skirtingų ports tad Program.cs nustačiau, jog jis pasilestų visą laiką ant to paties:
builder.WebHost.UseUrls("https://localhost:7204/");

Pastarajam URL frontend'as ir šaudo API užklausas, URL pasiekia iš .env failo:
VITE_API_URL=https://localhost:7204

Šią informaciją įdedu, jeigu port'ai visgi pasikeistų, kad būtų aiškų, kur juos keisti.
