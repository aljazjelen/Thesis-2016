

$(document).ready(setLayerArea);
$(document).ready(setProbeArea);
$(document).ready(setCutsArea);

$('#layersNumber').change(setLayerArea);
function setLayerArea(){
    var layersNumber = $('#layersNumber').val();
    $('#mesh_layers_error').html("");
    if (layersNumber == 0) {
        $('#layer_1st').hide();
        $('#layer_2nd').hide();
        $('#layer_3rd').hide();
    } else if (layersNumber == 1) {
        $('#layer_1st').show();
        $('#layer_2nd').hide();
        $('#layer_3rd').hide();
    } else if (layersNumber == 2) {
        $('#layer_1st').show();
        $('#layer_2nd').show();
        $('#layer_3rd').hide();
    } else if (layersNumber == 3) {
        $('#layer_1st').show();
        $('#layer_2nd').show();
        $('#layer_3rd').show();
    } else {
        $('#layer_1st').hide();
        $('#layer_2nd').hide();
        $('#layer_3rd').hide();
        $('#mesh_layers_error').html("For now only up to 3 layers are supported!");
    }
}

$(document).ready(setProbeArea);
$('#numProbes').change(setProbeArea);
function setProbeArea() {
    var layersNumber = $('#numProbes').val();
    $('#visualization_probes_error').html("");
    if (layersNumber == 0) {
        $('#probe_1st').hide();
        $('#probe_2nd').hide();
        $('#probe_3rd').hide();
    } else if (layersNumber == 1) {
        $('#probe_1st').show();
        $('#probe_2nd').hide();
        $('#probe_3rd').hide();
    } else if (layersNumber == 2) {
        $('#probe_1st').show();
        $('#probe_2nd').show();
        $('#probe_3rd').hide();
    } else if (layersNumber == 3) {
        $('#probe_1st').show();
        $('#probe_2nd').show();
        $('#probe_3rd').show();
    } else {
        $('#probe_1st').hide();
        $('#probe_2nd').hide();
        $('#probe_3rd').hide();
        $('#visualization_probes_error').html("For now only up to 3 probes are supported!");
    }
}

$(document).ready(setCutsArea);
$('#numCuts').change(setCutsArea);
function setCutsArea() {
    var layersNumber = $('#numCuts').val();
    $('#visualization_cuts_error').html("");
    if (layersNumber == 0) {
        $('#cut_1st').hide();
        $('#cut_2nd').hide();
        $('#cut_3rd').hide();
    } else if (layersNumber == 1) {
        $('#cut_1st').show();
        $('#ut_2nd').hide();
        $('#cut_3rd').hide();
    } else if (layersNumber == 2) {
        $('#cut_1st').show();
        $('#ut_2nd').show();
        $('#cut_3rd').hide();
    } else if (layersNumber == 3) {
        $('#cut_1st').show();
        $('#ut_2nd').show();
        $('#cut_3rd').show();
    } else {
        $('#cut_1st').hide();
        $('#ut_2nd').hide();
        $('#cut_3rd').hide();
        $('#visualization_cuts_error').html("For now only up to 3 cutting planes are supported!");
    }
}


function loginPlease() {

    // Get the pleaseLoginModal
    var modal = document.getElementById('pleaseLoginModal');

    // Get the <span> element, used to close pleaseLoginModal
    var span = document.getElementsByClassName("close")[0];

    modal.style.display = "block";

    // When the user clicks on <span> (x), close the modal
    span.onclick = function() {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}