# RaceDay API — Part 2 (PROG6212)

RaceDay is an ASP.NET Core Web API prototype for South African running, walking and cycling events. It supports Organiser and Participant roles, SQL Server persistence through Entity Framework Core, server-side session authentication, Swagger, unit tests and GitHub Actions CI.

> **Academic integrity and status:** This repository starter was prepared with AI assistance. Review, understand, adapt and test it yourself, and disclose AI use according to the assignment brief. It has not been compiled in this environment because the .NET SDK is unavailable here. Do not claim the CI is green until you push the repository and verify a successful run. The brief says ZIP files are not accepted; submit the GitHub repository URL through ARC.

## Roles
- **Organiser:** create/update/delete their own events, manage categories for their events, view enrolments for their events, and record results for those enrolments.
- **Participant:** browse events/categories, maintain their own profile, enrol in a category, and view their own enrolments/results.

## Requirements
- .NET 8 SDK
- SQL Server / LocalDB
- Visual Studio 2022 with ASP.NET and web development workload (or another compatible .NET IDE)

## Database setup
The default connection string in `RaceDay.API/appsettings.json` uses `(localdb)\\MSSQLLocalDB`. Change it to your installed SQL Server instance if necessary. Keep credentials out of source control.

From the repository root:

```bash
dotnet restore RaceDay.API/RaceDay.API.csproj
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialCreate --project RaceDay.API/RaceDay.API.csproj --startup-project RaceDay.API/RaceDay.API.csproj
dotnet ef database update --project RaceDay.API/RaceDay.API.csproj --startup-project RaceDay.API/RaceDay.API.csproj
dotnet run --project RaceDay.API/RaceDay.API.csproj
```

Open the Swagger URL printed by the app. Swagger is enabled in Development.

## Endpoints
| Method | Route | Access |
|---|---|---|
| POST | `/api/auth/register` | Public |
| POST | `/api/auth/login` | Public; establishes session cookie |
| POST | `/api/auth/logout` | Clears current session |
| GET | `/api/auth/me` | Authenticated |
| GET, PUT | `/api/profile` | Authenticated user's profile |
| GET | `/api/events` | Public |
| GET | `/api/events/{id}` | Public |
| POST | `/api/events` | Organiser; creates own event |
| PUT, DELETE | `/api/events/{id}` | Organiser; own events only |
| GET | `/api/events/{eventId}/categories` | Public |
| POST | `/api/events/{eventId}/categories` | Organiser; own events only |
| POST | `/api/enrolments` | Participant |
| GET | `/api/enrolments/my` | Participant |
| GET | `/api/enrolments/event/{eventId}` | Organiser; own events only |
| POST | `/api/results` | Organiser; own event enrolments only |
| GET | `/api/results/my` | Participant |

## Test

```bash
dotnet test RaceDay.API.Tests/RaceDay.API.Tests.csproj
```

The included tests verify password hashing and password verification. Add integration tests for registration/login, unauthenticated requests, role rejection, event management, and enrolments before submission, as required by the rubric.

## GitHub Actions

Workflow: `.github/workflows/part2-ci.yml`. Push the project to the lecturer-provided repository and inspect **Actions**. Include a screenshot of a genuinely successful green run below before submission.

**CI screenshot:** _Add your own screenshot after the workflow succeeds._

**Unlisted YouTube demonstration:** _Add your own video link after recording the running API, endpoints, authentication, code structure and tests._

## Notes to resolve before final submission
1. Reconcile the model/schema with the approved Part 1 ERD and SQL script; Part 2's rubric explicitly requires them to match.
2. Confirm session cookies work in Swagger/browser testing; cross-origin clients may require additional cookie/CORS configuration.
3. Add comprehensive endpoint integration tests and run the full build/test locally and in GitHub Actions.
4. Make at least 20 meaningful commits through your own GitHub account, reflecting actual development.
