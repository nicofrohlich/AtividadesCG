#version 450 core

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoord;
in vec3 VertexColor;

out vec4 FragColor;

uniform sampler2D tex_buffer;
uniform vec3 lightPos;        // Posição da luz
uniform vec3 viewPos;         // Posição da câmera (para o cálculo especular)
uniform vec3 lightColor;      // Cor da luz

void main()
{
    // Propriedades do material
    vec3 ambientColor = texture(tex_buffer, TexCoord).rgb;
    vec3 diffuseColor = texture(tex_buffer, TexCoord).rgb;
    vec3 specularColor = vec3(1.0, 1.0, 1.0);
    float shininess = 32.0;

    // Componente ambiente
    float ambientStrength = 0.3; // Aumente de 0.1 para 0.3 (ou outro valor maior)
    vec3 ambient = ambientStrength * lightColor * ambientColor;

    // Componente difusa
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor * diffuseColor;

    // Componente especular
    float specularStrength = 0.7; // Aumente de 0.5 para 0.7 (ou outro valor maior)
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), shininess);
    vec3 specular = specularStrength * spec * lightColor * specularColor;

    // Combinação final
    vec3 result = (ambient + diffuse + specular) * 1.2; // Multiplique por 1.2 para clarear

    FragColor = vec4(result, 1.0);
}