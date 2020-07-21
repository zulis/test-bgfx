#include "bigg.hpp"
// #include "entt/entt.hpp"
#include <entt/entity/registry.hpp>

class ExampleImguiDemo : public bigg::Application
{
    /* Entity Registry */
    entt::registry m_registry{};

    void onReset()
    {
        bgfx::setViewClear(0, BGFX_CLEAR_COLOR | BGFX_CLEAR_DEPTH, 0xc0c0c0ff, 1.0f, 0);
        bgfx::setViewRect(0, 0, 0, uint16_t(getWidth()), uint16_t(getHeight()));
    }

    void update(float dt)
    {
        bgfx::touch(0);
        // ImGui::ShowDemoWindow();
    }

public:
    ExampleImguiDemo() : bigg::Application("Test 1") {}
};

int main(int argc, char **argv)
{
    ExampleImguiDemo app;
    return app.run(argc, argv);
}