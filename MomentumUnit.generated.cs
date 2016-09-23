namespace Gu.Units
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A type for the unit <see cref="Gu.Units.Momentum"/>.
	/// Contains logic for conversion and formatting.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(MomentumUnitTypeConverter))]
    public struct MomentumUnit : IUnit, IUnit<Momentum>, IEquatable<MomentumUnit>
    {
        /// <summary>
        /// The NewtonSecond unit
        /// Contains logic for conversion and formatting.
        /// </summary>
        public static readonly MomentumUnit NewtonSecond = new MomentumUnit(newtonSecond => newtonSecond, newtonSecond => newtonSecond, "N⋅s");
        public static readonly MomentumUnit NewtonMeter = new MomentumUnit(newtonMeter => newtonMeter, newtonMeter => newtonMeter, "Nm");
        public static readonly MomentumUnit NewtonCentemeter = new MomentumUnit(newtonCentemeter => newtonCentemeter / 100, newtonMeter => newtonMeter * 100, "Ncm");
        public static readonly MomentumUnit Foot_Pound = new MomentumUnit(foot_pound => foot_pound / 0.7376, newtonMeter => newtonMeter * 0.7376, "ft-lb");
        public static readonly MomentumUnit Inch_Pound = new MomentumUnit(inch_pound => inch_pound / 8.85, newtonMeter => newtonMeter * 8.85, "in-lb");
        public static readonly MomentumUnit Kilogramm_Meter = new MomentumUnit(kilogramm_meter => kilogramm_meter / 0.10197, newtonMeter => newtonMeter * 0.10197, "kg-m");
        public static readonly MomentumUnit Kilogramm_Centemeter = new MomentumUnit(kilogramm_centemeter => kilogramm_centemeter / 10.197, newtonMeter => newtonMeter * 10.197, "kg-cm");
        

        private readonly Func<double, double> toNewtonMeter;
        private readonly Func<double, double> fromNewtonMeter;
        internal readonly string symbol;

        /// <summary>
        /// Initializes a new instance of <see cref="MomentumUnit"/>.
        /// </summary>
        /// <param name="toNewtonMeter">The conversion to <see cref="NewtonSecond"/></param>
        /// <param name="fromNewtonMeter">The conversion to <paramref name="symbol"/></param>
        /// <param name="symbol">The symbol for the <see cref="NewtonSecond"/></param>
        public MomentumUnit(Func<double, double> toNewtonMeter, Func<double, double> fromNewtonMeter, string symbol)
        {
            this.toNewtonMeter = toNewtonMeter;
            this.fromNewtonMeter = fromNewtonMeter;
            this.symbol = symbol;
        }

        /// <summary>
        /// The symbol for the <see cref="Gu.Units.MomentumUnit"/>.
        /// </summary>
        public string Symbol => this.symbol;

        /// <summary>
        /// The default unit for <see cref="Gu.Units.MomentumUnit"/>
        /// </summary>
        public MomentumUnit SiUnit => NewtonSecond;

        /// <summary>
        /// The default <see cref="Gu.Units.IUnit"/> for <see cref="Gu.Units.MomentumUnit"/>
        /// </summary>
        IUnit IUnit.SiUnit => NewtonSecond;

        /// <summary>
        /// Multiplies <paramref name="left"/> with <paramref name="right"/>
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>The <see cref="Momentum"/> that is the result from the multiplication.</returns>
        public static Momentum operator *(double left, MomentumUnit right)
        {
            return Momentum.From(left, right);
        }

        /// <summary>
        /// Indicates whether two <see cref="Gu.Units.MomentumUnit"/> instances are equal.
        /// </summary>
        /// <returns>
        /// true if the quantitys of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
        /// </returns>
        /// <param name="left">An instance of <see cref="Gu.Units.MomentumUnit"/>.</param>
        /// <param name="right">An instance of <see cref="Gu.Units.MomentumUnit"/>.</param>
	    public static bool operator ==(MomentumUnit left, MomentumUnit right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether two <see cref="Gu.Units.MomentumUnit"/> instances are not equal.
        /// </summary>
        /// <returns>
        /// true if the quantitys of <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.
        /// </returns>
        /// <param name="left">An instance of <see cref="Gu.Units.MomentumUnit"/>.</param>
        /// <param name="right">An instance of <see cref="Gu.Units.MomentumUnit"/>.</param>
        public static bool operator !=(MomentumUnit left, MomentumUnit right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Constructs a <see cref="MomentumUnit"/> from a string.
        /// Leading and trailing whitespace characters are allowed.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>An instance of <see cref="MomentumUnit"/></returns>
        public static MomentumUnit Parse(string text)
        {
            return UnitParser<MomentumUnit>.Parse(text);
        }

        /// <summary>
        /// Creates an instance of <see cref="Gu.Units.MomentumUnit"/> from its string representation
        /// </summary>
        /// <param name="text">The string representation of the <see cref="Gu.Units.MomentumUnit"/></param>
        /// <param name="result">The parsed <see cref="MomentumUnit"/></param>
        /// <returns>True if an instance of <see cref="MomentumUnit"/> could be parsed from <paramref name="text"/></returns>	
        public static bool TryParse(string text, out MomentumUnit result)
        {
            return UnitParser<MomentumUnit>.TryParse(text, out result);
        }

        /// <summary>
        /// Converts <paramref name="value"/> to NewtonSecond.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The converted value</returns>
        public double ToSiUnit(double value)
        {
            return this.toNewtonMeter(value);
        }

        /// <summary>
        /// Converts a value from newtonMeter.
        /// </summary>
        /// <param name="newtonMeter">The value in NewtonSecond</param>
        /// <returns>The converted value</returns>
        public double FromSiUnit(double newtonMeter)
        {
            return this.fromNewtonMeter(newtonMeter);
        }

        /// <summary>
        /// Creates a quantity with this unit
        /// </summary>
        /// <param name="value">The scalar value"</param>
        /// <returns>new Momentum(<paramref name="value"/>, this)</returns>
        public Momentum CreateQuantity(double value)
        {
            return new Momentum(value, this);
        }

        /// <summary>
        /// Gets the scalar value of <paramref name="quantity"/> in NewtonSecond
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public double GetScalarValue(Momentum quantity)
        {
            return FromSiUnit(quantity.newtonSecond);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.symbol;
        }

        /// <summary>
        /// Converts the unit value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">The format to use when convereting</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public string ToString(string format)
        {
            MomentumUnit unit;
            var paddedFormat = UnitFormatCache<MomentumUnit>.GetOrCreate(format, out unit);
            if (unit != this)
            {
                return format;
            }

            using (var builder = StringBuilderPool.Borrow())
            {
                builder.Append(paddedFormat.PrePadding);
                builder.Append(paddedFormat.Format);
                builder.Append(paddedFormat.PostPadding);
                return builder.ToString();
            }
        }

        /// <summary>
        /// Converts the unit value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="symbolFormat">Specifies the symbol format to use when creating the string representation.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public string ToString(SymbolFormat symbolFormat)
        {
            var paddedFormat = UnitFormatCache<MomentumUnit>.GetOrCreate(this, symbolFormat);
            using (var builder = StringBuilderPool.Borrow())
            {
                builder.Append(paddedFormat.PrePadding);
                builder.Append(paddedFormat.Format);
                builder.Append(paddedFormat.PostPadding);
                return builder.ToString();
            }
        }

        /// <summary>
        /// Returns a quantity indicating whether this instance is equal to a specified <see cref="Gu.Units.MomentumUnit"/> object.
        /// </summary>
        /// <param name="other">An instance of <see cref="Gu.Units.MomentumUnit"/> object to compare with this instance.</param>
        /// <returns>
        /// true if <paramref name="other"/> represents the same MomentumUnit as this instance; otherwise, false.
        /// </returns>
		public bool Equals(MomentumUnit other)
        {
            return this.symbol == other.symbol;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is MomentumUnit && Equals((MomentumUnit)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (this.symbol == null)
            {
                return 0; // Needed due to default ctor
            }

            return this.symbol.GetHashCode();
        }
    }
}