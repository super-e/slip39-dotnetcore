﻿using System.Security.Cryptography;

namespace superenrico.slip39_dotnetcore.model;
    public struct FiniteFieldElement
    {
        private byte _value = 0;
        public static int Order => 256;
        public FiniteFieldElement()
        {
        }

        public FiniteFieldElement(byte inputValue)
        {
            _value = inputValue;
        }

        static public implicit operator byte(FiniteFieldElement finiteFieldElement) => finiteFieldElement._value;
        static public implicit operator FiniteFieldElement(byte inputValue) => new FiniteFieldElement(inputValue);

        static public FiniteFieldElement operator +(FiniteFieldElement a, FiniteFieldElement b) => new FiniteFieldElement((byte)(a._value ^ b._value));
        static public FiniteFieldElement operator -(FiniteFieldElement a, FiniteFieldElement b) => new FiniteFieldElement((byte)(a._value ^ b._value));
        static public FiniteFieldElement operator *(FiniteFieldElement a, FiniteFieldElement b)
        {
            if (a == 0 || b == 0) return 0;
            return new FiniteFieldElement((byte)AntiLogTable283[((LogTable283[a._value] + LogTable283[b._value]) % (Order - 1))]);
        }

        static public FiniteFieldElement operator /(FiniteFieldElement a, FiniteFieldElement b)
        {
            if (b == 0) throw new ArgumentOutOfRangeException("Divisor cannot be 0");
            if (a == 0) return 0;
            var byteResult = (byte)AntiLogTable283[(((Order - 1) + LogTable283[a._value] - LogTable283[b._value]) % (Order - 1))];
            return new FiniteFieldElement(byteResult);

        }





        static public Dictionary<int, int> LogTable283 = new Dictionary<int, int>()
        {
            {1,0},
            {2,25},
            {3,1},
            {4,50},
            {5,2},
            {6,26},
            {7,198},
            {8,75},
            {9,199},
            {10,27},
            {11,104},
            {12,51},
            {13,238},
            {14,223},
            {15,3},
            {16,100},
            {17,4},
            {18,224},
            {19,14},
            {20,52},
            {21,141},
            {22,129},
            {23,239},
            {24,76},
            {25,113},
            {26,8},
            {27,200},
            {28,248},
            {29,105},
            {30,28},
            {31,193},
            {32,125},
            {33,194},
            {34,29},
            {35,181},
            {36,249},
            {37,185},
            {38,39},
            {39,106},
            {40,77},
            {41,228},
            {42,166},
            {43,114},
            {44,154},
            {45,201},
            {46,9},
            {47,120},
            {48,101},
            {49,47},
            {50,138},
            {51,5},
            {52,33},
            {53,15},
            {54,225},
            {55,36},
            {56,18},
            {57,240},
            {58,130},
            {59,69},
            {60,53},
            {61,147},
            {62,218},
            {63,142},
            {64,150},
            {65,143},
            {66,219},
            {67,189},
            {68,54},
            {69,208},
            {70,206},
            {71,148},
            {72,19},
            {73,92},
            {74,210},
            {75,241},
            {76,64},
            {77,70},
            {78,131},
            {79,56},
            {80,102},
            {81,221},
            {82,253},
            {83,48},
            {84,191},
            {85,6},
            {86,139},
            {87,98},
            {88,179},
            {89,37},
            {90,226},
            {91,152},
            {92,34},
            {93,136},
            {94,145},
            {95,16},
            {96,126},
            {97,110},
            {98,72},
            {99,195},
            {100,163},
            {101,182},
            {102,30},
            {103,66},
            {104,58},
            {105,107},
            {106,40},
            {107,84},
            {108,250},
            {109,133},
            {110,61},
            {111,186},
            {112,43},
            {113,121},
            {114,10},
            {115,21},
            {116,155},
            {117,159},
            {118,94},
            {119,202},
            {120,78},
            {121,212},
            {122,172},
            {123,229},
            {124,243},
            {125,115},
            {126,167},
            {127,87},
            {128,175},
            {129,88},
            {130,168},
            {131,80},
            {132,244},
            {133,234},
            {134,214},
            {135,116},
            {136,79},
            {137,174},
            {138,233},
            {139,213},
            {140,231},
            {141,230},
            {142,173},
            {143,232},
            {144,44},
            {145,215},
            {146,117},
            {147,122},
            {148,235},
            {149,22},
            {150,11},
            {151,245},
            {152,89},
            {153,203},
            {154,95},
            {155,176},
            {156,156},
            {157,169},
            {158,81},
            {159,160},
            {160,127},
            {161,12},
            {162,246},
            {163,111},
            {164,23},
            {165,196},
            {166,73},
            {167,236},
            {168,216},
            {169,67},
            {170,31},
            {171,45},
            {172,164},
            {173,118},
            {174,123},
            {175,183},
            {176,204},
            {177,187},
            {178,62},
            {179,90},
            {180,251},
            {181,96},
            {182,177},
            {183,134},
            {184,59},
            {185,82},
            {186,161},
            {187,108},
            {188,170},
            {189,85},
            {190,41},
            {191,157},
            {192,151},
            {193,178},
            {194,135},
            {195,144},
            {196,97},
            {197,190},
            {198,220},
            {199,252},
            {200,188},
            {201,149},
            {202,207},
            {203,205},
            {204,55},
            {205,63},
            {206,91},
            {207,209},
            {208,83},
            {209,57},
            {210,132},
            {211,60},
            {212,65},
            {213,162},
            {214,109},
            {215,71},
            {216,20},
            {217,42},
            {218,158},
            {219,93},
            {220,86},
            {221,242},
            {222,211},
            {223,171},
            {224,68},
            {225,17},
            {226,146},
            {227,217},
            {228,35},
            {229,32},
            {230,46},
            {231,137},
            {232,180},
            {233,124},
            {234,184},
            {235,38},
            {236,119},
            {237,153},
            {238,227},
            {239,165},
            {240,103},
            {241,74},
            {242,237},
            {243,222},
            {244,197},
            {245,49},
            {246,254},
            {247,24},
            {248,13},
            {249,99},
            {250,140},
            {251,128},
            {252,192},
            {253,247},
            {254,112},
            {255,7},
        };
        static public Dictionary<int, int> AntiLogTable283 = new Dictionary<int, int>()
        {
            {0,1},
            {1,3},
            {2,5},
            {3,15},
            {4,17},
            {5,51},
            {6,85},
            {7,255},
            {8,26},
            {9,46},
            {10,114},
            {11,150},
            {12,161},
            {13,248},
            {14,19},
            {15,53},
            {16,95},
            {17,225},
            {18,56},
            {19,72},
            {20,216},
            {21,115},
            {22,149},
            {23,164},
            {24,247},
            {25,2},
            {26,6},
            {27,10},
            {28,30},
            {29,34},
            {30,102},
            {31,170},
            {32,229},
            {33,52},
            {34,92},
            {35,228},
            {36,55},
            {37,89},
            {38,235},
            {39,38},
            {40,106},
            {41,190},
            {42,217},
            {43,112},
            {44,144},
            {45,171},
            {46,230},
            {47,49},
            {48,83},
            {49,245},
            {50,4},
            {51,12},
            {52,20},
            {53,60},
            {54,68},
            {55,204},
            {56,79},
            {57,209},
            {58,104},
            {59,184},
            {60,211},
            {61,110},
            {62,178},
            {63,205},
            {64,76},
            {65,212},
            {66,103},
            {67,169},
            {68,224},
            {69,59},
            {70,77},
            {71,215},
            {72,98},
            {73,166},
            {74,241},
            {75,8},
            {76,24},
            {77,40},
            {78,120},
            {79,136},
            {80,131},
            {81,158},
            {82,185},
            {83,208},
            {84,107},
            {85,189},
            {86,220},
            {87,127},
            {88,129},
            {89,152},
            {90,179},
            {91,206},
            {92,73},
            {93,219},
            {94,118},
            {95,154},
            {96,181},
            {97,196},
            {98,87},
            {99,249},
            {100,16},
            {101,48},
            {102,80},
            {103,240},
            {104,11},
            {105,29},
            {106,39},
            {107,105},
            {108,187},
            {109,214},
            {110,97},
            {111,163},
            {112,254},
            {113,25},
            {114,43},
            {115,125},
            {116,135},
            {117,146},
            {118,173},
            {119,236},
            {120,47},
            {121,113},
            {122,147},
            {123,174},
            {124,233},
            {125,32},
            {126,96},
            {127,160},
            {128,251},
            {129,22},
            {130,58},
            {131,78},
            {132,210},
            {133,109},
            {134,183},
            {135,194},
            {136,93},
            {137,231},
            {138,50},
            {139,86},
            {140,250},
            {141,21},
            {142,63},
            {143,65},
            {144,195},
            {145,94},
            {146,226},
            {147,61},
            {148,71},
            {149,201},
            {150,64},
            {151,192},
            {152,91},
            {153,237},
            {154,44},
            {155,116},
            {156,156},
            {157,191},
            {158,218},
            {159,117},
            {160,159},
            {161,186},
            {162,213},
            {163,100},
            {164,172},
            {165,239},
            {166,42},
            {167,126},
            {168,130},
            {169,157},
            {170,188},
            {171,223},
            {172,122},
            {173,142},
            {174,137},
            {175,128},
            {176,155},
            {177,182},
            {178,193},
            {179,88},
            {180,232},
            {181,35},
            {182,101},
            {183,175},
            {184,234},
            {185,37},
            {186,111},
            {187,177},
            {188,200},
            {189,67},
            {190,197},
            {191,84},
            {192,252},
            {193,31},
            {194,33},
            {195,99},
            {196,165},
            {197,244},
            {198,7},
            {199,9},
            {200,27},
            {201,45},
            {202,119},
            {203,153},
            {204,176},
            {205,203},
            {206,70},
            {207,202},
            {208,69},
            {209,207},
            {210,74},
            {211,222},
            {212,121},
            {213,139},
            {214,134},
            {215,145},
            {216,168},
            {217,227},
            {218,62},
            {219,66},
            {220,198},
            {221,81},
            {222,243},
            {223,14},
            {224,18},
            {225,54},
            {226,90},
            {227,238},
            {228,41},
            {229,123},
            {230,141},
            {231,140},
            {232,143},
            {233,138},
            {234,133},
            {235,148},
            {236,167},
            {237,242},
            {238,13},
            {239,23},
            {240,57},
            {241,75},
            {242,221},
            {243,124},
            {244,132},
            {245,151},
            {246,162},
            {247,253},
            {248,28},
            {249,36},
            {250,108},
            {251,180},
            {252,199},
            {253,82},
            {254,246},
        };
    }
