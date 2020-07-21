add_executable( test1 main.cpp )
target_link_libraries( test1 bigg EnTT )
set_target_properties( test1 PROPERTIES FOLDER "examples" )