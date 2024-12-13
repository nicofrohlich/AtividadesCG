#version 450 core

layout (location = 0) in vec3 aPos;       // Posição dos vértices
layout (location = 1) in vec3 aColor;     // Cor dos vértices
layout (location = 2) in vec2 aTexCoord;  // Coordenadas de textura
layout (location = 3) in vec3 aNormal;    // Normais dos vértices

out vec3 FragPos;        // Posição do fragmento no espaço mundial
out vec3 Normal;         // Normal do fragmento no espaço mundial
out vec2 TexCoord;       // Coordenadas de textura
out vec3 VertexColor;    // Cor passada para o fragment shader

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0));
    Normal = mat3(transpose(inverse(model))) * aNormal; // Corrigir transformação da normal
    TexCoord = aTexCoord;
    VertexColor = aColor;

    gl_Position = projection * view * vec4(FragPos, 1.0);
}
