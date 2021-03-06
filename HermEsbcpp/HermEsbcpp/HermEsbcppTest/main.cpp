#define BOOST_ALL_DYN_LINK
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <glog/logging.h>
#include <gtest/gtest.h>


int main(int argc, char* argv[])
{
    // Initialize Google's logging library.
    google::InitGoogleLogging(argv[0]);
	testing::InitGoogleTest(&argc, argv);
	
    return RUN_ALL_TESTS();
}
