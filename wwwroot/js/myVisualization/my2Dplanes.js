
function set2DContent(content,object) {

    /*   Content variable (.json format)
        - velocity x or y or z
        - pressure

         Object variable
        - 3d models in vtk style with .txt format
    */

    // Routine to get URL of server location
    var win_protocol = window.location.protocol;
    var win_hostname = window.location.hostname;
    var win_port = window.location.port;
    var filePath = "";

    // Routine to assemble path to chosen file
    if (win_protocol.length > 1) {
        filePath = filePath + win_protocol;
        if (win_hostname.length > 1) {
            filePath = filePath + "\\\\" + win_hostname;
        }
        if (win_port.length > 1) {
            filePath = filePath + ":" + win_port;
        }
    }

    filePath = filePath + document.getElementById("hiddenPath").innerHTML;
    filePath = filePath + "2DVisualizer\\";


    // This is VTK loader example implemented into BufferGeometry loader
    // There are 2 separete loaders:
    //  - BufferGeometryLoader
    if (!Detector.webgl) { Detector.addGetWebGLMessage(); }

    var container, stats;
    var camera, scene, renderer, lut, legendLayout;
    var position;
    var mesh;

    var colorMap;
    var numberOfColors;

    //Added by Aljaz
    var material_color;


    // Properties for dimensional viewing
    var dimension_properties = {
        dimension2D: {
            noZoom: false,
            noRotate: true,
            noPan: false
        },
        dimension3D: {
            noZoom: false,
            noRotate: false,
            noPan: false
        },
    };



    init();
    animate();

    function init() {

        container_name = '2D_content_canvas';
        container = document.getElementById(container_name);
        $("#" + container_name).children().remove();
   
        var custom_width = container.clientWidth;
        // SCENE
        scene = new THREE.Scene();

        // CAMERA
        camera = new THREE.PerspectiveCamera(20, window.innerWidth / window.innerHeight, 1, 10000);
        camera.name = 'camera';
        scene.add(camera);


        controls = new THREE.TrackballControls(camera);

        controls.rotateSpeed = 5.0;
        controls.zoomSpeed = 5;
        controls.panSpeed = 2;

        controls.noZoom = false;
        controls.noPan = false;
        controls.noRotate = true;

        controls.staticMoving = true;
        controls.dynamicDampingFactor = 0.3;




        stats = new Stats();
        stats.domElement.style.position = 'absolute';
        stats.domElement.style.bottom = '0px';
        stats.domElement.style.zIndex = 100;
        container.appendChild(stats.domElement);

        // LIGHT
        var ambientLight = new THREE.AmbientLight(0x999999);
        ambientLight.name = 'ambientLight';
        scene.add(ambientLight);

        colorMap = 'rainbow';
        numberOfColors = 512;

        legendLayout = 'vertical';

        loadModel(colorMap, numberOfColors, legendLayout);

        camera.position.x = 0;
        camera.position.y = 0;
        camera.position.z = 10;

        var directionalLight1 = new THREE.DirectionalLight(0xffffff, 0.7);
        directionalLight1.position.x = 17;
        directionalLight1.position.y = 9;
        directionalLight1.position.z = 30;
        directionalLight1.name = 'directionalLight';
        scene.add(directionalLight1);

        var directionalLight2 = new THREE.DirectionalLight(0xffffff, 0.7);
        directionalLight2.position.x = -100;
        directionalLight2.position.y = -30;
        directionalLight2.position.z = -5;
        directionalLight2.name = 'directionalLight';
        scene.add(directionalLight2);


        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setClearColor(0xffffff);
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(custom_width, custom_width * window.innerHeight / window.innerWidth);
        container.appendChild(renderer.domElement);

        window.addEventListener('resize', onWindowResize, false);

        window.addEventListener("keydown", onKeyDown, true);
    }

    var rotWorldMatrix;

    function rotateAroundWorldAxis(object, axis, radians) {

        if (!axis) return;

        rotWorldMatrix = new THREE.Matrix4();
        rotWorldMatrix.makeRotationAxis(axis.normalize(), radians);
        rotWorldMatrix.multiply(object.matrix);

        object.matrix = rotWorldMatrix;
        object.rotation.setFromRotationMatrix(object.matrix);
    }

    function onWindowResize() {
        container = document.getElementById(container_name);
        var custom_width = container.clientWidth;

        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();

        controls.handleResize();
        renderer.setSize(custom_width, custom_width * window.innerHeight / window.innerWidth);

        render();
    }

    function animate() {
        requestAnimationFrame(animate);
        controls.update();
        stats.update();
        render();
    }

    function render() {
        //rotateAroundWorldAxis(mesh, position, Math.PI / 180);
        renderer.render(scene, camera);
    }

    function getMaxOfArray(numArray) {
        return Math.max.apply(null, numArray);
    }

    function getMinOfArray(numArray) {
        return Math.min.apply(null, numArray);
    }

    function loadModel(colorMap, numberOfColors, legendLayout) {

        // Obtain color with BufferGeometryLoader
        // obtained by converter: convertFile_to_JSON
        var loader = new THREE.BufferGeometryLoader();
        loader.load(filePath + content + ".json", function (color_geometry) {

            // Read pressure array from converted JSON
            temp_material_color = color_geometry.attributes.pressure.array;
            material_color = temp_material_color;

            // Obtain geometry with VTKLoader
            // VTK sampling file, must rename extension!
            var loaderVTK = new THREE.VTKLoader();
            loaderVTK.load(filePath + object + '.txt', function (geometry) {
            //loaderVTK.load(filePath + object + '.txt', function (obtained_geometry) {
                //obtained_geometry.computeVertexNormals();
                //obtained_geometry.center();


                //geometry = new THREE.Geometry().fromBufferGeometry(obtained_geometry);
                //geometry.mergeVertices()
                geometry.computeVertexNormals();
                //geometry.mergeVertices()

                //max_pos = Number(getMaxOfArray(obtained_geometry.attributes.position.array));
                //min_pos = Number(getMinOfArray(obtained_geometry.attributes.position.array));

                geometry.colorsNeedUpdate = true;

                //var material = new THREE.MeshLambertMaterial({ color: 0xffffff, side: THREE.DoubleSide, vertexColors: THREE.FaceColors, shading: THREE.SmoothShading });
                var material = new THREE.MeshLambertMaterial({ color: 0xffffff, side: THREE.DoubleSide, vertexColors: THREE.VertexColors, shading: THREE.SmoothShading });

                var lutColors = [];

                lut = new THREE.Lut(colorMap, numberOfColors);

                //lut.setMax(100);  // for CUT
                lut.setMax(2500);  // for LEFT
                lut.setMin(-3500);

                console.log("Material color length: ", material_color.length)

                for (var i = 0; i < material_color.length; i++) {

                    var colorValue = material_color[i];
                    color = lut.getColor(colorValue);

                    if (color == undefined) {

                        console.log("ERROR: " + colorValue);

                    } else {

                        lutColors[3 * i] = color.r;
                        lutColors[3 * i + 1] = color.g;
                        lutColors[3 * i + 2] = color.b;

                        //face = geometry.faces[i];
                        //face.color.setRGB(color.r, color.g, color.b);
                    }
                }

                geometry.addAttribute('color', new THREE.BufferAttribute(new Float32Array(lutColors), 3));


                mesh = new THREE.Mesh(geometry, material);

                geometry.computeBoundingBox();
                var boundingBox = geometry.boundingBox;
                var center = boundingBox.center();

                if (position === undefined) {
                    position = new THREE.Vector3(center.x, center.y, center.z);
                }
                scene.add(mesh);

                if (legendLayout) {
                    var camera_x = camera.getWorldPosition().x;
                    var camera_y = camera.getWorldPosition().y;
                    var camera_z = camera.getWorldPosition().z;
                    var mesh_x = mesh.getWorldPosition().x;
                    var mesh_y = mesh.getWorldPosition().y;
                    var mesh_z = mesh.getWorldPosition().z;


                    var leg_position = [];
                    leg_position[0] = (camera_x + mesh_x) / 5;
                    leg_position[1] = (camera_y + mesh_y) / 5;
                    leg_position[2] = (camera_z + mesh_z) / 5;

                    if (legendLayout == 'horizontal') {
                        legend = lut.setLegendOn({ 'layout': 'horizontal', 'position': { 'x': leg_position[0], 'y': leg_position[1], 'z': leg_position[2] } });
                    }
                    else {

                        legend = lut.setLegendOn();
                    }
                    scene.add(legend);

                    var labels = lut.setLegendLabels({ 'title': 'Pressure', 'um': 'Pa', 'ticks': 5 });

                    scene.add(labels['title']);

                    for (var i = 0; i < Object.keys(labels['ticks']).length; i++) {
                        scene.add(labels['ticks'][i]);
                        scene.add(labels['lines'][i]);
                    }
                }
            });
        });

    }

    function cleanScene() {
        var elementsInTheScene = scene.children.length;
        for (var i = elementsInTheScene - 1; i > 0; i--) {
            if (scene.children[i].name != 'camera' &&
                scene.children[i].name != 'ambientLight' &&
                scene.children[i].name != 'directionalLight') {
                scene.remove(scene.children[i]);
            }
        }
    }

    function onKeyDown(e) {
        var maps = ['rainbow', 'cooltowarm', 'blackbody', 'grayscale'];
        var colorNumbers = ['16', '128', '256', '512'];

        if (e.keyCode == 65) {
            cleanScene();
            var index = maps.indexOf(colorMap) >= maps.length - 1 ? 0 : maps.indexOf(colorMap) + 1;
            colorMap = maps[index];
            loadModel(colorMap, numberOfColors, legendLayout);
        } else if (e.keyCode == 83) {
            cleanScene();
            var index = colorNumbers.indexOf(numberOfColors) >= colorNumbers.length - 1 ? 0 : colorNumbers.indexOf(numberOfColors) + 1;
            numberOfColors = colorNumbers[index];
            loadModel(colorMap, numberOfColors, legendLayout);
        } else if (e.keyCode == 68) {
            if (!legendLayout) {
                cleanScene();
                legendLayout = 'vertical';
                loadModel(colorMap, numberOfColors, legendLayout);
            } else {
                cleanScene();
                legendLayout = lut.setLegendOff();
                loadModel(colorMap, numberOfColors, legendLayout);
            }
        } else if (e.keyCode == 70) {
            cleanScene();
            if (!legendLayout) return false;
            lut.setLegendOff();
            if (legendLayout == 'horizontal') {
                legendLayout = 'vertical';
            } else {
                legendLayout = 'horizontal';
            }
            loadModel(colorMap, numberOfColors, legendLayout);
        }
    }





}   // end of set2DContent() function