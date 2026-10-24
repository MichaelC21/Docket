# Docket
A background job queue in ASP.NET Core


## Known limitations

- **Claimed jobs are never reclaimed.** A worker claims a job by atomically setting its status to `Running` and recording `ClaimedBy`/`ClaimedAt`. If that worker crashes or stops before finishing, the job stays `Running` and no other worker picks it up. Recovery of stale claims (e.g. a lease timeout based on `ClaimedAt`) is not implemented yet but will be in the future.
