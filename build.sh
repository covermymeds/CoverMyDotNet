xbuild
test_success=$(nunit-console CoverMyDotNet.Tests/bin/Debug/CoverMyDotNet.Tests.dll | grep "Failures: 0");
if [-z test_success]
	echo "Tests failed";