FROM mcr.microsoft.com/dotnet/sdk:9.0

# Install dependencies for Playwright (Linux). These packages are recommended by Playwright docs.
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       ca-certificates \
       fonts-liberation \
       libasound2 \
       libatk-bridge2.0-0 \
       libatk1.0-0 \
       libc6 \
       libcairo2 \
       libcap2-bin \
       libdbus-1-3 \
       libdrm2 \
       libexpat1 \
       libgbm1 \
       libgcc1 \
       libglib2.0-0 \
       libgtk-3-0 \
       libnspr4 \
       libnss3 \
       libpango-1.0-0 \
       libx11-6 \
       libxcomposite1 \
       libxdamage1 \
       libxext6 \
       libxfixes3 \
       libxrandr2 \
       wget \
       xvfb \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src

# Copy solution and restore
COPY . ./
RUN dotnet restore

# Install Playwright CLI and browsers
RUN dotnet tool install --global Microsoft.Playwright.CLI \
    && export PATH="$PATH:$HOME/.dotnet/tools" \
    && playwright install --with-deps

# Build
RUN dotnet build -c Release

# Default command: run tests (UI tests disabled by default)
CMD ["/bin/bash", "-lc", "dotnet test --logger \"trx;LogFileName=TestResults.trx\""]
