/*
Template Name: Samply - Admin & Dashboard Template
Author: Pichforest
Website: https://Pichforest.com/
Contact: Pichforest@gmail.com
File: crm dashboard Js File
*/

// mini-1
var options1 = {
  series: [{
      data: [25, 40, 31, 42, 51, 44, 36, 50, 37, 29, 25, 40, 31, 42, 51,]
  }],
  fill: {
    colors: ['#0576b9'],
  },
  chart: {
      type: 'bar',
      height: 82,
      width: 310,
      sparkline: {
          enabled: true
      },
  },
  plotOptions: {
      bar: {
          columnWidth: '60%',
          borderRadius: 0,
      }
  },

  labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
  xaxis: {
      crosshairs: {
          width: 1
      },
  },
  tooltip: {
      fixed: {
          enabled: false
      },
      x: {
          show: false
      },
      y: {
          title: {
              formatter: function (seriesName) {
                  return ''
              }
          }
      },
      marker: {
          show: false
      }
  }
};

var chart1 = new ApexCharts(document.querySelector("#mini-1"), options1);
chart1.render();


// sparkline 1
var sparklineoptions1 = {
  series: [{
    data: [12, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14]
  }],
  chart: {
    type: 'area',
    width: 120,
    height: 45,
    sparkline: {
      enabled: true
    }
  }, 
  fill: {
    type: 'gradient',
    gradient: {
      shadeIntensity: 1,
      inverseColors: false,
      opacityFrom: 0.45,
      opacityTo: 0.05,
      stops: [20, 100, 100, 100]
    },
  },
  stroke: {
    curve: 'smooth',
    width: 2,
  },
  colors: ['#0576b9'],
  tooltip: {
    fixed: {
      enabled: false
    },
    x: {
      show: false
    },
    y: {
      title: {
        formatter: function (seriesName) {
          return ''
        }
      }
    },
    marker: {
      show: false
    }
  }
};

var sparklinechart1 = new ApexCharts(document.querySelector("#chart-sparkline1"), sparklineoptions1);
sparklinechart1.render();

//  sparkline 2
var sparklineoptions1 = {
  series: [{
    data: [65, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14]
  }],
  chart: {
    type: 'area',
    width: 120,
    height: 45,
    sparkline: {
      enabled: true
    }
  }, 
  fill: {
    type: 'gradient',
    gradient: {
      shadeIntensity: 1,
      inverseColors: false,
      opacityFrom: 0.45,
      opacityTo: 0.05,
      stops: [20, 100, 100, 100]
    },
  },
  stroke: {
    curve: 'smooth',
    width: 2,
  },
  colors: ['#f56e50'],
  tooltip: {
    fixed: {
      enabled: false
    },
    x: {
      show: false
    },
    y: {
      title: {
        formatter: function (seriesName) {
          return ''
        }
      }
    },
    marker: {
      show: false
    }
  }
};

var sparklinechart1 = new ApexCharts(document.querySelector("#chart-sparkline2"), sparklineoptions1);
sparklinechart1.render();


//  sparkline 3
var sparklineoptions1 = {
  series: [{
    data: [12, 75, 2, 47, 42, 15, 47, 75, 65, 19, 14]
  }],
  chart: {
    type: 'area',
    width: 120,
    height: 45,
    sparkline: {
      enabled: true
    }
  }, 
  fill: {
    type: 'gradient',
    gradient: {
      shadeIntensity: 1,
      inverseColors: false,
      opacityFrom: 0.45,
      opacityTo: 0.05,
      stops: [20, 100, 100, 100]
    },
  },
  stroke: {
    curve: 'smooth',
    width: 2,
  },
  colors: ['#0576b9'],
  tooltip: {
    fixed: {
      enabled: false
    },
    x: {
      show: false
    },
    y: {
      title: {
        formatter: function (seriesName) {
          return ''
        }
      }
    },
    marker: {
      show: false
    }
  }
};

var sparklinechart1 = new ApexCharts(document.querySelector("#chart-sparkline3"), sparklineoptions1);
sparklinechart1.render();

// Donut Chart
var options = {
  series: [40, 60, 40, 25],
  chart: {
    height: 248,
      type: 'donut',
  },
  labels: ["Invest", "Income", "Spends", "Installment"],
  plotOptions: {
      pie: {
        donut: {
          size: '75%',
          
        }
      }
  },
  legend: {
      show: false,
  },
  
  colors: ['#2cb57e', '#0576b9', '#f5bd58', '#f56e50'],

};

var chart = new ApexCharts(document.querySelector("#donut-chart"), options);
chart.render();


// Range Column Chart



var options = {
  series: [{
    name: 'Profit',
    data: [{
      x: '2:00PM',
      y: [450, 850]
    }, {
      x: '2:30PM',
      y: [370, 750]
    }, {
      x: '3:00PM',
      y: [550, 800]
    }, {
      x: '3:30PM',
      y: [500, 900]
    }, {
      x: '4:00PM',
      y: [400, 750]
    }, {
      x: '4:30PM',
      y: [550, 800]
    }, {
      x: '5:00PM',
      y: [500, 800]
    }, {
      x: '5:30PM',
      y: [400, 700]
    }, {
      x: '6:00PM',
      y: [500, 750]
    }, {
      x: '6:30PM',
      y: [380, 750]
    }, {
      x: '7:00PM',
      y: [550, 850]
    }, {
      x: '7:30PM',
      y: [600, 950]
    }, {
      x: '8:00PM',
      y: [500, 900]
    }]
  },
  {
    name: 'Loss',
    data: [{
      x: '1',
      y: [500, 750]
    }, {
      x: '2',
      y: [550, 900]
    }, {
      x: '3',
      y: [500, 680]
    }, {
      x: '4',
      y: [400, 800]
    }, {
      x: '5',
      y: [550, 850]
    }, {
      x: '6',
      y: [650, 750]
    }, {
      x: '7',
      y: [650, 850]
    }, {
      x: '8',
      y: [500, 750]
    }, {
      x: '9',
      y: [600, 850]
    }, {
      x: '10',
      y: [450, 900]
    }, {
      x: '11',
      y: [370, 750]
    }, {
      x: '12',
      y: [550, 650]
    }, {
      x: '13',
      y: [700, 950]
    }]
    }],
  chart: {
    type: 'rangeBar',
    height: 400,
    toolbar: {
      show: false,
    }
  },
  plotOptions: {
    bar: {
      horizontal: false,
      columnWidth: '42%',
      dataLabels: {
        position: 'top',
      },
    }
  },
  stroke: {
    show: true,
    width: 4,
    colors: '#ffffff',
  },

  dataLabels: {
    enabled: false
  },
  legend: {
    show: false,
  },

  tooltip: {
    shared: true,
    intersect: false
  },
  xaxis: {
    categories: ['2:00PM', '2:30PM', '3:00PM', '3:30PM', '4:00PM', '4:30PM', '5:00PM', '5:30PM', '6:00PM', '6:30PM', '7:00PM', '7:30PM', '8:00PM'],
  },

  colors: ['#0576b9', '#f56e50'],
};

var chart = new ApexCharts(document.querySelector("#column_range"), options);
chart.render();




// Chart-1
var options1 = {
  series: [{
  data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
  
},
colors: ['#f56e50'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-1"), options1);
chart1.render();

// Chart-2
var options1 = {
  series: [{
  data: [50, 15, 35, 62, 23, 56, 44, 12, 36, 9, 54]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
  
},
colors: ['#2cb57e'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-2"), options1);
chart1.render();

// Chart-3
var options1 = {
  series: [{
  data: [25, 35, 35, 89, 63, 25, 44, 12, 36, 9, 54]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
  
},
colors: ['#2cb57e'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-3"), options1);
chart1.render();


// Chart-4
var options1 = {
  series: [{
  data: [50, 15, 35, 34, 23, 56, 65, 41, 36, 41, 32]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
  
},
colors: ['#f56e50'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-4"), options1);
chart1.render();



// Chart-5
var options1 = {
  series: [{
  data: [45, 53, 24, 89, 63, 60, 36, 50, 36, 32, 54]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
},
colors: ['#f56e50'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-5"), options1);
chart1.render();

// Chart-6
var options1 = {
  series: [{
  data: [50, 15, 35, 62, 23, 56, 44, 12, 36, 9, 54]
}],
  chart: {
  type: 'line',
  width: 120,
  height: 30,
  sparkline: {
    enabled: true
  }
  
},
colors: ['#2cb57e'],
stroke: {
  curve: 'smooth',
  width: 3,
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#chart-6"), options1);
chart1.render();
// MAp
$('#sales-by-locations').vectorMap({
    map: 'world_mill_en',
    normalizeFunction: 'polynomial',
    hoverOpacity: 0.7,
    hoverColor: false,
    regionStyle: {
        initial: {
            fill: '#dee5f1'
        }
    },
    markerStyle: {
        initial: {
            r: 9,
            'fill': '#2cb57e',
            'fill-opacity': 0.9,
            'stroke': '#fff',
            'stroke-width': 7,
            'stroke-opacity': 0.4
        },

        hover: {
            'stroke': '#fff',
            'fill-opacity': 1,
            'stroke-width': 1.5
        }
    },
    backgroundColor: 'transparent',
    markers: [{
        latLng: [41.90, 12.45],
        name: 'USA'
    }, {
        latLng: [12.05, -61.75],
        name: 'Russia'
    }, {
        latLng: [1.3, 103.8],
        name: 'Australia'
    }]
});



// Radial chart 1
var options= {
  series: [70],
  chart: {
      height: 150, type: 'radialBar',
  }
  ,
  plotOptions: {
      radialBar: {
         
          hollow: {
              size: '50%',
          }
          ,
          dataLabels: {
              name: {
                  show: false,
              }
              ,
              value: {
                  show: true, fontSize: '14px', offsetY: 5,
              },
              style: {
                  colors: ['#fff']
              }
          }
      }
      ,
  }
  ,
  colors: ['#2cb57e'],
}

;
var chart=new ApexCharts(document.querySelector("#card-limit"), options);
chart.render();





var swiper = new Swiper(".slider", {
  slidesPerView: 1,
  navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
  },
  breakpoints: {
      768: {
          slidesPerView: 3,
      },
      1024: {
          slidesPerView: 4,
      },
  },
}); 





