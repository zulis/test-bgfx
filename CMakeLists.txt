# cmake_minimum_required( VERSION 3.0 )
# set(CMAKE_CXX_STANDARD 14)
# set(CMAKE_CXX_STANDARD_REQUIRED ON)

# project(bigged)

# add_subdirectory(external/bigg/deps)
# include(external/bigg/deps/bgfx.cmake/cmake/util/ConfigureDebugging.cmake)
# add_library( bigg STATIC external/bigg/src/bigg.cpp external/bigg/src/bigg_assets.h external/bigg/src/bigg_imgui.hpp external/bigg/src/bigg_shaders.hpp external/bigg/include/bigg.hpp )
# target_link_libraries( bigg PUBLIC bgfx imgui glfw glm )
# target_include_directories( bigg PUBLIC include )

option(BIGG_EXAMPLES OFF)
set(BIGG_ASSET_GEN OFF)
add_subdirectory(external/bigg)
add_subdirectory(external/entt)
add_subdirectory(examples)


# link_libraries(
#         bgfx
#         imgui
#         glfw
#         glm
#         )

# include_directories(bigged
#         PUBLIC
#         external/bigg/include
#         external/entt/src/entt
#         )

# add_library(bigged
#         external/bigg/src/bigg.cpp
#         external/bigg/deps/bgfx.cmake/bgfx/examples/common/bounds.cpp
#         external/bigg/deps/bgfx.cmake/bgfx/examples/common/debugdraw/debugdraw.cpp
#         src/bigged.cpp
#         )