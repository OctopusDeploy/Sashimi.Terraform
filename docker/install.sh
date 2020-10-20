#!/bin/bash

set -e

wait_file() {
  local file="$1"; shift
  local wait_seconds="${1:-10}"; shift # 10 seconds as default timeout

  until test $((wait_seconds--)) -eq 0 -o -e "$file" ; do sleep 1; done

  ((++wait_seconds))
}

wait_file "/publish/Sashimi.Terraform.Tests/Sashimi.Terraform.Tests.dll" 60 || {
  echo "/publish/Sashimi.Terraform.Tests/Sashimi.Terraform.Tests.dll not found"
  exit 1
}

wait_file "/publish/Calamari.Terraform.Tests/linux-x64/Calamari.Terraform.Tests.dll" 60 || {
  echo "/publish/Calamari.Terraform.Tests/linux-x64/Calamari.Terraform.Tests.dll not found"
  exit 1
}

sleep 5

cp -a /publish/Sashimi.Terraform.Tests/. /Octopus/Sashimi.Terraform.Tests
cp -a /publish/Calamari.Terraform.Tests/linux-x64/. /Octopus/Calamari.Terraform.Tests

exec dotnet vstest /Octopus/Sashimi.Terraform.Tests/Sashimi.Terraform.Tests.dll --TestCaseFilter:"TestCategory != Windows" --logger:trx
exec dotnet vstest /Octopus/Calamari.Terraform.Tests/Calamari.Terraform.Tests.dll --TestCaseFilter:"TestCategory != Windows" --logger:trx