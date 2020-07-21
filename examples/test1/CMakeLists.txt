set(EXEC_NAME test1)
add_executable(${EXEC_NAME} main.cpp )
target_link_libraries(${EXEC_NAME} bigg EnTT::EnTT )
set_target_properties(${EXEC_NAME} PROPERTIES FOLDER "examples" )