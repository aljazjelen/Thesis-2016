

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
filePath = filePath + "tsv\\";


var content_ID;
var content_file;

function setGraphContent(content) {
    // ID of dom element to where graph is rendered
    content_ID = '#plot_region';

    // delete already rendered graphs
    $(content_ID).children().remove();

    // content of file!
    content_file = content + ".tsv";


    var margin = { top: 20, right: 80, bottom: 35, left: 50 },
        width = 960 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

    var parseDate = d3.time.format("%Y%m%d").parse;


    //THIS IS CHANGED
    var x = d3.scale.linear()
        .range([0, width]);

    var y = d3.scale.linear()
        .range([height, 0]);

    var color = d3.scale.category10();

    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom")
        .innerTickSize(-height)
        .outerTickSize(0)
        .tickPadding(10);

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .innerTickSize(-width)
        .outerTickSize(0)
        .tickPadding(10);

    var line = d3.svg.line()
        .interpolate("basis")
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.probeValue); });


    var svg = d3.select(content_ID).append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
      .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");


    d3.tsv(filePath + content_file, function (error, data) {
        if (error) throw error;

        color.domain(d3.keys(data[0]).filter(function (key) { return key !== "date"; }));

        data.forEach(function (d) {
            d.date = +(d.date);
        });

        var probes = color.domain().map(function (name) {
            return {
                name: name,
                values: data.map(function (d) {
                    return { date: d.date, probeValue: +d[name] };
                })
            };
        });

        x.domain([
          d3.min(probes, function (c) { return d3.min(c.values, function (v) { return v.date; }); }),
          d3.max(probes, function (c) { return d3.max(c.values, function (v) { return v.date; }); })
        ]);

        y.domain([
          d3.min(probes, function (c) { return d3.min(c.values, function (v) { return v.probeValue; }); }),
          d3.max(probes, function (c) { return d3.max(c.values, function (v) { return v.probeValue; }); })
        ]);

        svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(xAxis)
            .append("text")
            .attr("x", width)
            .attr("y", -10)
            .style("text-anchor", "middle")
            .text("Time (s)");

        svg.append("g")
            .attr("class", "y axis")
            .call(yAxis)
            .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            //.text("Pressure (Pa)");
            .text(content_file);

        var probe = svg.selectAll(".probe")
            .data(probes)
            .enter().append("g")
            .attr("class", "probe");

        probe.append("path")
            .attr("class", "line")
            .attr("d", function (d) { return line(d.values); })
            .style("stroke", function (d) { return color(d.name); });

        probe.append("text")
            .datum(function (d) { return { name: d.name, value: d.values[d.values.length - 1] }; })
            .attr("transform", function (d) { return "translate(" + x(d.value.date) + "," + y(d.value.probeValue) + ")"; })
            .attr("x", 3)
            .attr("dy", ".35em")
            .text(function (d) { return d.name; });
    });



}   // end of setContent function